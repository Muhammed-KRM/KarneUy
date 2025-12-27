import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface Classroom {
  id: number;
  name: string;
  teacherId: number;
  joinCode: string;
}

@Component({
  selector: 'app-class-list',
  templateUrl: './class-list.component.html',
  styleUrls: ['./class-list.component.css']
})
export class ClassListComponent implements OnInit {
  classes: Classroom[] = [];
  // Dummy Data for Preview, replaced by API call usually
  // In real app, inject ClassroomService

  constructor(private router: Router, private http: HttpClient) { }

  ngOnInit(): void {
    // Mock Data loading
    this.classes = [
      { id: 1, name: '10-A Matematik', teacherId: 1, joinCode: 'A1B2' },
      { id: 2, name: '11-B Fizik', teacherId: 1, joinCode: 'C3D4' }
    ];
  }

  openChat(classId: number) {
    this.router.navigate(['/classroom/chat', classId]);
  }

  createClass() {
    // TODO: Material Dialog for Create
    alert('Create Class Feature');
  }
}
