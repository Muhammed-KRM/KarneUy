import { ComponentFixture, TestBed } from '@angular/core/testing';

import { QuestionEditorComponent } from './question-editor.component';

describe('QuestionEditorComponent', () => {
  let component: QuestionEditorComponent;
  let fixture: ComponentFixture<QuestionEditorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [QuestionEditorComponent]
    });
    fixture = TestBed.createComponent(QuestionEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
