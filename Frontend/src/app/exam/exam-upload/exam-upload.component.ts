import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-exam-upload',
  templateUrl: './exam-upload.component.html',
  styleUrls: ['./exam-upload.component.css']
})
export class ExamUploadComponent {
  selectedFile: File | null = null;
  message: string = '';
  isUploading: boolean = false;

  // Configuration for parser (Mock)
  parserConfig = {
    startRow: 1,
    studentNoStart: 0,
    studentNoLength: 9,
    answersStart: 10,
    answersLength: 40
  };

  constructor(private http: HttpClient) { }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  upload() {
    if (!this.selectedFile) return;

    this.isUploading = true;
    const formData = new FormData();
    formData.append('file', this.selectedFile);
    formData.append('config', JSON.stringify(this.parserConfig));

    // this.http.post('/api/exams/1/upload-results', formData).subscribe(...)

    // Mock Success
    setTimeout(() => {
      this.message = 'Sınav başarıyla yüklendi ve analiz edildi!';
      this.isUploading = false;
    }, 1500);
  }
}
