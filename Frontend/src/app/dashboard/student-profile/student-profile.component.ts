import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Chart, ChartConfiguration, registerables } from 'chart.js';

Chart.register(...registerables);

@Component({
  selector: 'app-student-profile',
  templateUrl: './student-profile.component.html',
  styleUrls: ['./student-profile.component.css']
})
export class StudentProfileComponent implements OnInit {
  chart: any;
  loading: boolean = true;
  noData: boolean = false;

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.fetchData();
  }

  fetchData() {
    this.http.get<any[]>('http://localhost:5246/api/StudentStats/history').subscribe({
      next: (data) => {
        if (!data || data.length === 0) {
          this.noData = true;
          this.loading = false;
          return;
        }

        this.createChart(data);
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.loading = false;
      }
    });
  }

  createChart(data: any[]) {
    const labels = data.map(d => d.date.split('T')[0]); // Simple date fmt
    const scores = data.map(d => d.score);

    const config: ChartConfiguration = {
      type: 'line',
      data: {
        labels: labels,
        datasets: [{
          label: 'Sınav Puanı',
          data: scores,
          borderColor: '#2FA84F', // Itten Green
          backgroundColor: 'rgba(47, 168, 79, 0.2)',
          fill: true,
          tension: 0.4
        }]
      },
      options: {
        responsive: true,
        plugins: {
          legend: { position: 'top' },
          title: { display: true, text: 'Akademik Başarım Grafiği' }
        },
        scales: {
          y: { beginAtZero: true, max: 100 }
        }
      }
    };

    this.chart = new Chart('scoreType', config);
  }
}
