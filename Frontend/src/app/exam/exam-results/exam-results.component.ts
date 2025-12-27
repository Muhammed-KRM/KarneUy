import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-exam-results',
  templateUrl: './exam-results.component.html',
  styleUrls: ['./exam-results.component.css']
})
export class ExamResultsComponent implements OnInit {
  results: any[] = [];
  loading: boolean = true;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    // Reuse the history endpoint for list, or a dedicated one.
    // Using history for simplicity as it returns recent exams.
    this.http.get<any[]>('http://localhost:5246/api/StudentStats/history').subscribe({
      next: (data) => {
        this.results = data;
        this.loading = false;
      },
      error: () => this.loading = false
    });
  }

  downloadReport(examName: string) {
    // We need ExamId. The history endpoint returned ExamName and Date but maybe not Id?
    // Let's assume for this mock that we can't easily get the ID from that specific endpoint or we update it.
    // Actually, let's update StudentStatsController to return ExamId too.
    // For now, I'll alert.
    alert('Rapor indirme servisi ExamId gerektiriyor. Backend güncellenmeli.');
  }

  // Improved Version: Fetch with ExamId
  // I will update the Controller in the next step to include ExamId, then this will work.
  downloadReportWithId(examId: number) {
    window.open(`http://localhost:5246/api/reports/exam/${examId}`, '_blank');
  }
}
