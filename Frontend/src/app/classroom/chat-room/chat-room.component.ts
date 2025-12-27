import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

interface Message {
  id: number;
  senderName: string;
  senderId: number;
  content: string;
  sentAt: Date;
  isMe: boolean;
}

@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.css']
})
export class ChatRoomComponent implements OnInit, OnDestroy {
  messages: Message[] = [];
  newMessage: string = '';
  classId: number = 0;
  chatName: string = 'Sınıf Sohbeti';
  currentUserId: number = 0;

  private intervalId: any;

  constructor(private route: ActivatedRoute, private auth: AuthService) { }

  ngOnInit(): void {
    this.classId = Number(this.route.snapshot.paramMap.get('id'));
    this.currentUserId = this.auth.currentUserValue?.id || 0;

    // Load Initial Mock Data
    this.loadMessages();

    // Poll for new messages every 3s
    this.intervalId = setInterval(() => this.loadMessages(), 3000);
  }

  ngOnDestroy(): void {
    if (this.intervalId) clearInterval(this.intervalId);
  }

  loadMessages() {
    // Mock Data simulating API response
    // In real app: this.http.get(`/api/classrooms/${classId}/messages`)
    const mockData = [
      { id: 1, senderName: 'Ahmet Hoca', senderId: 101, content: 'Yarın sınav var arkadaşlar, unutmayın!', sentAt: new Date(), isMe: false },
      { id: 2, senderName: 'Ben', senderId: this.currentUserId, content: 'Hocam hangi konulardan sorumluyuz?', sentAt: new Date(), isMe: true }
    ];

    // Logic to merge/update
    this.messages = mockData;
  }

  sendMessage() {
    if (!this.newMessage.trim()) return;

    // Optimistic Update
    this.messages.push({
      id: Date.now(),
      senderName: 'Ben',
      senderId: this.currentUserId,
      content: this.newMessage,
      sentAt: new Date(),
      isMe: true
    });

    // TODO: API Call
    console.log('Sending:', this.newMessage);
    this.newMessage = '';

    // Auto scroll to bottom logic usually goes here
  }
}
