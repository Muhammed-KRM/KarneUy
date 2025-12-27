import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './dashboard/home/home.component';
import { AuthGuard } from './core/guards/auth.guard';
import { ClassListComponent } from './classroom/class-list/class-list.component';
import { ChatRoomComponent } from './classroom/chat-room/chat-room.component';
import { ExamUploadComponent } from './exam/exam-upload/exam-upload.component';
import { StudentProfileComponent } from './dashboard/student-profile/student-profile.component';
import { ExamResultsComponent } from './exam/exam-results/exam-results.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'classroom/list', component: ClassListComponent, canActivate: [AuthGuard] },
  { path: 'classroom/chat/:id', component: ChatRoomComponent, canActivate: [AuthGuard] },
  { path: 'exam/upload', component: ExamUploadComponent, canActivate: [AuthGuard] },
  { path: 'profile', component: StudentProfileComponent, canActivate: [AuthGuard] },
  { path: 'exam/results', component: ExamResultsComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
