import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';

export interface Notification {
    id: number;
    title: string;
    message: string;
    type: string;
    isRead: boolean;
    createdAt: Date;
}

@Injectable({
    providedIn: 'root'
})
export class NotificationService {
    private apiUrl = 'http://localhost:5246/api/notifications';

    private unreadSubject = new BehaviorSubject<Notification[]>([]);
    public unread$ = this.unreadSubject.asObservable();

    constructor(private http: HttpClient) {
        // Initial Load
        this.pollNotifications();
        // Poll every 30 seconds
        setInterval(() => this.pollNotifications(), 30000);
    }

    pollNotifications() {
        this.http.get<Notification[]>(`${this.apiUrl}/unread`).subscribe(data => {
            this.unreadSubject.next(data);
        });
    }

    markAsRead(id: number) {
        this.http.post(`${this.apiUrl}/${id}/read`, {}).subscribe(() => {
            // Optimistic update
            const current = this.unreadSubject.value.filter(n => n.id !== id);
            this.unreadSubject.next(current);
        });
    }
}
