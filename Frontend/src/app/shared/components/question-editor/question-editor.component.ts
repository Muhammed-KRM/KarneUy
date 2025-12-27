import { Component, EventEmitter, Output } from '@angular/core';
import { QuestionService } from 'src/app/core/services/question.service';

@Component({
  selector: 'app-question-editor',
  templateUrl: './question-editor.component.html',
  styleUrls: ['./question-editor.component.css']
})
export class QuestionEditorComponent {
  content: string = '';
  selectedFile: File | null = null;
  previewUrl: string | null = null;
  uploadedUrl: string | null = null;

  isUploading: boolean = false;
  isSubmitting: boolean = false;

  @Output() questionCreated = new EventEmitter<void>();

  constructor(private questionService: QuestionService) { }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;

      // Preview
      const reader = new FileReader();
      reader.onload = () => this.previewUrl = reader.result as string;
      reader.readAsDataURL(file);

      // Auto Upload to get URL (or do it on submit)
      this.uploadImage();
    }
  }

  uploadImage() {
    if (!this.selectedFile) return;
    this.isUploading = true;
    this.questionService.uploadImage(this.selectedFile).subscribe({
      next: (res) => {
        this.uploadedUrl = res.url; // "http://localhost.../uploads/..." ideally absolute or relative
        // Since backend returns /uploads/name, we might need full URL if frontend is different domain?
        // But for images <img src="/uploads..."> works if hosted same domain OR we prepend backend URL.
        // Let's prepend if needed or keep relative if proxy setup. 
        // For now, let's assume raw usage:
        this.uploadedUrl = `http://localhost:5246${res.url}`;
        this.isUploading = false;
      },
      error: () => this.isUploading = false
    });
  }

  submit() {
    if (!this.content && !this.uploadedUrl) return;

    this.isSubmitting = true;
    const payload = {
      content: this.content,
      imageUrl: this.uploadedUrl,
      // lessonId: ... (Feature for later: Select Lesson)
    };

    this.questionService.createQuestion(payload).subscribe({
      next: () => {
        alert('Soru paylaşıldı!');
        this.content = '';
        this.selectedFile = null;
        this.previewUrl = null;
        this.uploadedUrl = null;
        this.isSubmitting = false;
        this.questionCreated.emit();
      },
      error: (err) => {
        alert('Hata: ' + err.message);
        this.isSubmitting = false;
      }
    });
  }
}
