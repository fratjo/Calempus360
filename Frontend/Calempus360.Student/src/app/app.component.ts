import { AsyncPipe } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { ClassroomService } from './services/classroom.service';
import { CourseService } from './services/course.service';
import { SessionService } from './services/session.service';
import { StudentGroupsService } from './services/student-groups.service';
import { FullCalendarModule } from '@fullcalendar/angular';
import { CalendarOptions } from '@fullcalendar/core/index.js';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';
import { UniversityService } from './services/university.service';
import { AcademicYearService } from './services/academic-year.service';

@Component({
  selector: 'app-root',
  imports: [FullCalendarModule, AsyncPipe],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  private sessionService = inject(SessionService);
  // for selection
  private academicYearService = inject(AcademicYearService);
  public academicYears$ = this.academicYearService.academicYears$;
  private universityService = inject(UniversityService);
  public universities$ = this.universityService.universities$;
  private studentGroupService = inject(StudentGroupsService);
  public studentGroups$ = this.studentGroupService.studentGroups$;
  private classroomService = inject(ClassroomService);
  public classrooms$ = this.classroomService.classrooms$;
  private courseService = inject(CourseService);
  public courses$ = this.courseService.courses$;
  currentPopover: HTMLDivElement | null = null;
  private readonly router = inject(Router);

  ngOnInit(): void {
    this.universityService.getUniversities().subscribe();
    this.academicYearService.getAcademicYears().subscribe((ays) => {
      const aYid = ays.find(
        (ay) =>
          new Date() >= new Date(ay.dateStart!) &&
          new Date() <= new Date(ay.dateEnd!),
      )?.id;

      if (aYid) {
        sessionStorage.setItem('academicYear', JSON.stringify(aYid));
      }
    });
  }

  calendarOptions: CalendarOptions = {
    initialView: 'timeGridWeek',
    plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
    headerToolbar: {
      left: 'prev,next',
      center: 'title',
      right: 'today,timeGridDay,timeGridWeek,dayGridMonth', // user can switch between the two
    },
    views: {
      dayGridMonth: {
        titleFormat: { year: 'numeric', month: 'long' },
      },
      timeGrid: {
        titleFormat: { year: 'numeric', month: 'long', day: 'numeric' },
      },
    },
    nowIndicator: true,
    weekends: false,
    selectable: true,
    timeZone: 'Europe/Paris',
    allDaySlot: false,
    slotDuration: '00:30:00',
    slotMinTime: '07:00:00',
    slotMaxTime: '21:30:00',

    eventMouseLeave: (arg) => {
      this.removePopover(); // Remove the popover when the mouse leaves the event
    },
    eventMouseEnter: (arg) => {
      this.removePopover(); // Remove any existing popover
      this.sessionService.getSessionById(arg.event.id).subscribe((session) => {
        const popover = document.createElement('div');
        popover.className = 'popover';
        popover.style.position = 'absolute';
        popover.style.background = '#fff';
        popover.style.border = '1px solid #ccc';
        popover.style.padding = '10px';
        popover.style.boxShadow = '0 2px 10px rgba(0, 0, 0, 0.2)';
        popover.style.zIndex = '1000';
        popover.style.top = `${arg.jsEvent.clientY + 10}px`;
        popover.style.left = `${arg.jsEvent.clientX + 10}px`;
        popover.innerText = `Event: ${session.name}
          Start: ${session.dateTimeStart}
          End: ${session.dateTimeEnd}
          Classroom: ${session.classroom!.name}
          Course: ${session.course!.name}
          Student Groups: ${session.studentGroups!.map((sg: any) => sg.code).join(', ')}
          Equipments: ${session.equipments!.map((eq: any) => eq.name).join(', ')}`;
        this.currentPopover = popover; // Store the popover in a component variable
        document.body.appendChild(popover);
      });
    },
  };

  toggleWeekends() {
    this.calendarOptions.weekends = !this.calendarOptions.weekends; // toggle the boolean!
  }

  onUniversityChange(event: any) {
    const studentGroupSelect = document.querySelector(
      '#studentGroup',
    ) as HTMLSelectElement;
    if (studentGroupSelect) {
      studentGroupSelect.value = 'all';
    }

    sessionStorage.setItem('university', JSON.stringify(event.target.value));

    this.sessionService
      .getSessions({
        universityId: event.target.value == 'all' ? null : event.target.value,
        academicYearId: JSON.parse(sessionStorage.getItem('academicYear')!),
      })
      .subscribe((sessions) => {
        this.calendarOptions.events = sessions.map((session) => {
          return {
            id: session.id,
            title: session.name,
            start: session.dateTimeStart,
            end: session.dateTimeEnd,
            classroom: session.classroom,
            course: session.course,
            studentGroups: session.studentGroups,
            equipments: session.equipments,
          };
        });
      });

    this.studentGroupService.getStudentGroups();
  }

  onStudentGroupChange(event: any) {
    this.sessionService
      .getSessions({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
        academicYearId: JSON.parse(sessionStorage.getItem('academicYear')!),
        studentGroupId: event.target.value == 'all' ? null : event.target.value,
      })
      .subscribe((sessions) => {
        this.calendarOptions.events = sessions.map((session) => {
          return {
            id: session.id,
            title: session.name,
            start: session.dateTimeStart,
            end: session.dateTimeEnd,
            classroom: session.classroom,
            course: session.course,
            studentGroups: session.studentGroups,
            equipments: session.equipments,
          };
        });
      });
  }
  removePopover() {
    document.querySelectorAll('.popover')!.forEach((popover) => {
      if (popover instanceof HTMLDivElement) {
        popover.remove();
      }
    });
    if (this.currentPopover && document.body.contains(this.currentPopover)) {
      document.body.removeChild(this.currentPopover);
    }
    this.currentPopover = null; // Clear the stored popover
  }
}
