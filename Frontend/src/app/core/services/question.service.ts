import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Question {
  id: number;
  content: string;
  imageUrl?: string;
  lessonId?: number;
  topicId?: number;
  userId: number;
  createdAt: string;
  user?: any;
}

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  private apiUrl = 'http://localhost:5246/api/questions';
  private mediaUrl = 'http://localhost:5246/api/media/upload';

  constructor(private http: HttpClient) { }

  createQuestion(data: any): Observable<Question> {
    return this.http.post<Question>(this.apiUrl, data);
  }

  uploadImage(file: File): Observable<{ url: string }> {
    const formData = new FormData();
    formData.append('file', file);
    return this.http.post<{ url: string }>(this.mediaUrl, formData);
  }

  getMyQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.apiUrl}/my`);
  }

  deleteQuestion(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
