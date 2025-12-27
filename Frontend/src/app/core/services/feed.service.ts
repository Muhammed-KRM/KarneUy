import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Question } from './question.service'; // Reuse interface

@Injectable({
  providedIn: 'root'
})
export class FeedService {
  private apiUrl = 'http://localhost:5246/api/feed';

  constructor(private http: HttpClient) { }

  getHomeFeed(skip: number = 0, take: number = 20): Observable<Question[]> {
    const params = new HttpParams().set('skip', skip).set('take', take);
    return this.http.get<Question[]>(`${this.apiUrl}/home`, { params });
  }

  getExploreFeed(take: number = 20): Observable<Question[]> {
    const params = new HttpParams().set('take', take);
    return this.http.get<Question[]>(`${this.apiUrl}/explore`, { params });
  }
}
