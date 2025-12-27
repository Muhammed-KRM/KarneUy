import { Component, OnInit } from '@angular/core';
import { NotificationService, Notification } from '../../../core/services/notification.service';

@Component({
  selector: 'app-notification-menu',
  templateUrl: './notification-menu.component.html',
  styleUrls: ['./notification-menu.component.css']
})
export class NotificationMenuComponent implements OnInit {
  notifications: Notification[] = [];
  isOpen: boolean = false;

  constructor(private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.notificationService.unread$.subscribe(data => {
      this.notifications = data;
    });
  }

  toggle() {
    this.isOpen = !this.isOpen;
  }

  markRead(id: number) {
    this.notificationService.markAsRead(id);
  }
}
