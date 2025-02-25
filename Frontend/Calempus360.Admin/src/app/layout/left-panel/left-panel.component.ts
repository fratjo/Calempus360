import {
  Component,
  inject,
  Input,
  input,
  InputSignal,
  output,
  Signal,
  signal,
} from '@angular/core';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { RouterLink, RouterOutlet } from '@angular/router';
import { SelectUniversityDialogComponent } from '../../shared/components/select-university-dialog/select-university-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-left-panel',
  imports: [RouterOutlet, RouterLink, MatSidenavModule, MatIconModule],
  templateUrl: './left-panel.component.html',
  styleUrl: './left-panel.component.scss',
})
export class LeftPanelComponent {
  title = output<any>();
  university = input.required<any>();
  universities = input.required<any[]>();

  readonly dialog = inject(MatDialog);

  openUniversityChoice(): void {
    // Open the dialog
    const dialogRef = this.dialog.open(SelectUniversityDialogComponent, {
      data: { name: this.university(), universities: this.universities() },
    });

    // Handle the dialog result
    dialogRef.afterClosed().subscribe((result) => {
      console.log(
        `Dialog result: ${
          result ? result : 'User clicked cancel or clicked outside the dialog'
        }`
      );
      if (result) {
        this.title.emit(result);
      }
    });
  }
}
