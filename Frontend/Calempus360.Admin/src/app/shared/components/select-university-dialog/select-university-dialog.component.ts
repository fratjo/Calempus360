import {
  ChangeDetectionStrategy,
  Component,
  inject,
  model,
  signal,
} from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogActions,
  MatDialogClose,
  MatDialogContent,
  MatDialogRef,
  MatDialogTitle,
} from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';

interface DialogData {
  name: string;
  universities: string[];
}

@Component({
  selector: 'app-select-university-dialog',
  imports: [
    MatFormFieldModule,
    MatSelectModule,
    FormsModule,
    MatButtonModule,
    MatDialogContent,
    MatDialogActions,
    MatDialogClose,
  ],
  templateUrl: './select-university-dialog.component.html',
  styleUrl: './select-university-dialog.component.scss',
})
export class SelectUniversityDialogComponent {
  readonly dialogRef = inject(MatDialogRef<SelectUniversityDialogComponent>);
  readonly data = inject<DialogData>(MAT_DIALOG_DATA);
  readonly university = signal(this.data.name);
  readonly universities = model(this.data.universities);

  onNoClick(): void {
    this.dialogRef.close();
  }
}
