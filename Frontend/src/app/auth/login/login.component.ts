import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMsg: string = '';
  isLoading: boolean = false;

  constructor(private fb: FormBuilder, private auth: AuthService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) return;

    this.isLoading = true;
    this.errorMsg = '';

    this.auth.login(this.loginForm.value).subscribe({
      next: () => {
        // Redirect handled in service
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMsg = 'Login Failed. Check credentials.';
        this.isLoading = false;
        console.error(err);
      }
    });
  }
}
