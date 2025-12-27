import { Component, OnInit } from '@angular/core';
import { FeedService } from 'src/app/core/services/feed.service';
import { Question } from 'src/app/core/services/question.service';

@Component({
  selector: 'app-social-feed',
  templateUrl: './social-feed.component.html',
  styleUrls: ['./social-feed.component.css']
})
export class SocialFeedComponent implements OnInit {
  feed: Question[] = [];
  loading: boolean = true;
  error: string | null = null;
  mode: 'home' | 'explore' = 'home';

  constructor(private feedService: FeedService) { }

  ngOnInit(): void {
    this.loadFeed();
  }

  loadFeed() {
    this.loading = true;
    const req = this.mode === 'home'
      ? this.feedService.getHomeFeed()
      : this.feedService.getExploreFeed();

    req.subscribe({
      next: (data) => {
        this.feed = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Akış yüklenemedi: ' + err.message;
        this.loading = false;
      }
    });
  }

  toggleMode(mode: 'home' | 'explore') {
    if (this.mode === mode) return;
    this.mode = mode;
    this.loadFeed();
  }

  refresh() {
    this.loadFeed();
  }
}
