import { Component, inject, OnInit } from '@angular/core';
import { FullCalendarModule } from '@fullcalendar/angular';
import { Calendar, CalendarOptions } from '@fullcalendar/core/index.js';
import dayGridPlugin from '@fullcalendar/daygrid';
import interactionPlugin from '@fullcalendar/interaction';
import timeGridPlugin from '@fullcalendar/timegrid';
import { ClassroomService } from '../../core/services/classroom.service';
import { SessionService } from '../../core/services/session.service';
import { SiteService } from '../../core/services/site.service';
import { StudentGroupsService } from '../../core/services/student-groups.service';
import { CourseService } from '../../core/services/course.service';
import { AsyncPipe } from '@angular/common';
import { Session } from '../../core/models/session.interface';

@Component({
  selector: 'app-session',
  imports: [FullCalendarModule, AsyncPipe],
  templateUrl: './session.component.html',
  styleUrl: './session.component.scss',
})
export class SessionComponent implements OnInit {
  private sessionService = inject(SessionService);
  // for selection
  private studentGroupService = inject(StudentGroupsService);
  public studentGroups$ = this.studentGroupService.studentGroups$;
  private classroomService = inject(ClassroomService);
  public classrooms$ = this.classroomService.classrooms$;
  private courseService = inject(CourseService);
  public courses$ = this.courseService.courses$;
  currentPopover: HTMLDivElement | null = null;

  ngOnInit(): void {
    this.sessionService
      .getSessions({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
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

    this.classroomService
      .getClassrooms({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
      })
      .subscribe();

    this.studentGroupService.getStudentGroups();
    this.courseService.getCourses();
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
    editable: true,
    selectable: true,

    eventClick(arg) {
      alert('Event: ' + arg.event.title);
    },

    eventMouseEnter: (arg) => {
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

    eventMouseLeave: (arg) => {
      document.querySelectorAll('.popover')!.forEach((popover) => {
        if (popover instanceof HTMLDivElement) {
          popover.remove();
        }
      });
      if (this.currentPopover && document.body.contains(this.currentPopover)) {
        document.body.removeChild(this.currentPopover);
      }
      this.currentPopover = null; // Clear the stored popover
    },

    eventDragStart(arg) {
      console.log('eventDragStart', arg.event.start);
    },

    eventDragStop(arg) {
      console.log('eventDragStop', arg.event.start);
    },

    eventDrop: (arg) => {
      this.sessionService.getSessionById(arg.event.id).subscribe((session) => {
        if(session){
          session.dateTimeStart = arg.event.start!;
          console.log(arg.event.end);
          session.dateTimeEnd = arg.event.end!;
          session.classroom = session.classroom.id;
          session.course = session.course.id;
          session.studentGroups = session.studentGroups!.map((sg) => sg.id);
          session.equipments = session.equipments!.map((e) => e.id);
          this.sessionService.updateSessions(session).subscribe({
            next: (e) => console.log('Updated !'),
            error: (e) => {
              alert(e.error.detail);
              arg.revert();
            }
          });
        }
      })
        alert(
        arg.event.title +
          ' was dropped on ' +
          (arg.event.start ? arg.event.start : 'an unknown date'),
      );

      if (!confirm('Are you sure about this change?')) {
        arg.revert();
      }
    },

    eventResizeStart(arg) {
      console.log('eventResizeStart', arg.event.start);
    },

    eventResizeStop(arg) {
      console.log('eventResizeStop', arg.event.start);
    },
  };

  toggleWeekends() {
    this.calendarOptions.weekends = !this.calendarOptions.weekends; // toggle the boolean!
  }

  onClassroomChange(event: any) {
    const studentGroupSelect = document.querySelector(
      '#studentGroup',
    ) as HTMLSelectElement;
    if (studentGroupSelect) {
      studentGroupSelect.value = 'all';
    }
    const courseSelect = document.querySelector('#course') as HTMLSelectElement;
    if (courseSelect) {
      courseSelect.value = 'all';
    }

    this.sessionService
      .getSessions({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
        academicYearId: JSON.parse(sessionStorage.getItem('academicYear')!),
        classroomId: event.target.value == 'all' ? null : event.target.value,
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

  onStudentGroupChange(event: any) {
    const courseSelect = document.querySelector('#course') as HTMLSelectElement;
    if (courseSelect) {
      courseSelect.value = 'all';
    }
    const classroomSelect = document.querySelector(
      '#classroom',
    ) as HTMLSelectElement;
    if (classroomSelect) {
      classroomSelect.value = 'all';
    }

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

  onCourseChange(event: any) {
    const studentGroupSelect = document.querySelector(
      '#studentGroup',
    ) as HTMLSelectElement;
    if (studentGroupSelect) {
      studentGroupSelect.value = 'all';
    }
    const classroomSelect = document.querySelector(
      '#classroom',
    ) as HTMLSelectElement;
    if (classroomSelect) {
      classroomSelect.value = 'all';
    }

    this.sessionService
      .getSessions({
        universityId: JSON.parse(sessionStorage.getItem('university')!),
        academicYearId: JSON.parse(sessionStorage.getItem('academicYear')!),
        courseId: event.target.value == 'all' ? null : event.target.value,
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
}
