import { Component, signal } from '@angular/core';
import { TopBarComponent } from './layout/top-bar/top-bar.component';
import { LeftPanelComponent } from './layout/left-panel/left-panel.component';

@Component({
  selector: 'app-root',
  imports: [TopBarComponent, LeftPanelComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent {
  universities = signal<string[]>([
    'Stanford University',
    'University of California, Berkeley',
    'University of California, Los Angeles',
  ]);
  university = signal<string>(this.universities()[0]);

  onTitleChange(newTitle: string): void {
    if (!this.universities().includes(newTitle)) {
      this.universities().push(newTitle);
    }
    this.university.set(newTitle);
  }
}
