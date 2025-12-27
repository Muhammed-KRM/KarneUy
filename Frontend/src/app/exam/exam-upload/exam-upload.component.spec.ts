import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExamUploadComponent } from './exam-upload.component';

describe('ExamUploadComponent', () => {
  let component: ExamUploadComponent;
  let fixture: ComponentFixture<ExamUploadComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ExamUploadComponent]
    });
    fixture = TestBed.createComponent(ExamUploadComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
