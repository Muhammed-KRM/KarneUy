import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService, User } from '../../core/services/auth.service';
import { SocialFeedComponent } from '../../shared/components/social-feed/social-feed.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUser: User | null = null;
  sideMenuOpen: boolean = false;

  @ViewChild('feed') feedComponent!: SocialFeedComponent;

  constructor(private auth: AuthService) { }

  ngOnInit(): void {
    this.auth.user$.subscribe(u => this.currentUser = u);
  }

  logout() {
    this.auth.logout();
  }

  toggleMenu() {
    this.sideMenuOpen = !this.sideMenuOpen;
  }

  onQuestionCreated() {
    // Refresh feed
    if (this.feedComponent) {
      this.feedComponent.refresh();
    }
  }
}
