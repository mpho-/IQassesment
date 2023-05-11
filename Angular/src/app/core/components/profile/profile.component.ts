// profile.component.ts
import { Component } from '@angular/core';
import { User } from '../../models/User';
import { AuthService } from '../../services/AuthService';
import { UserService } from '../../services/UserService';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.less']
})
export class ProfileComponent {
  user!: User;
  profilePicture!: File;

  constructor(private authService: AuthService, private userService: UserService) {
    this.userService.getCurrentUser().subscribe(user => {
      this.user = user;
    });
  }

  onSubmit() {
    this.userService.updateUser(this.user).subscribe(() => {
      if (this.profilePicture) {
        const reader = new FileReader();
        reader.readAsDataURL(this.profilePicture);
        reader.onload = () => {
          //this.user.profilePicture = reader.result.toString();
        };
      }
    });
  }

  onFileSelected(event: any) {
    this.profilePicture = event.target.files[0];
  }
}