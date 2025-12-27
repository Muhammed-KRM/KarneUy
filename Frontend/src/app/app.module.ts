import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CustomInputComponent } from './shared/components/custom-input/custom-input.component';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './dashboard/home/home.component';
import { TokenInterceptor } from './core/interceptors/token.interceptor';
import { ClassListComponent } from './classroom/class-list/class-list.component';
import { ChatRoomComponent } from './classroom/chat-room/chat-room.component';
import { ExamUploadComponent } from './exam/exam-upload/exam-upload.component';
import { ExamResultsComponent } from './exam/exam-results/exam-results.component';
import { NotificationMenuComponent } from './shared/components/notification-menu/notification-menu.component';
import { StudentProfileComponent } from './dashboard/student-profile/student-profile.component';
import { QuestionEditorComponent } from './shared/components/question-editor/question-editor.component';
import { SocialFeedComponent } from './shared/components/social-feed/social-feed.component';

@NgModule({
  declarations: [
    AppComponent,
    CustomInputComponent,
    LoginComponent,
    HomeComponent,
    ClassListComponent,
    ChatRoomComponent,
    ExamUploadComponent,
    ExamResultsComponent,
    NotificationMenuComponent,
    StudentProfileComponent,
    QuestionEditorComponent,
    SocialFeedComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
