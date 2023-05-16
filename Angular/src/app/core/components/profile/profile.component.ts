import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { AuthService } from '../../services/AuthService';
import { UserService } from '../../services/UserService';
import { environment } from 'src/environments/environment';
import { LookUpService } from '../../services/LookupService';
import { Gender } from '../../models/Gender';
import { firstValueFrom } from 'rxjs';
import { Project } from '../../models/Project';
import { ProjectService } from '../../services/ProjectService';
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.less']
})
export class ProfileComponent implements OnInit {
  user: User = new User;
  profilePicture!: File;
  uploadUrl!: string;
  selectedGender!: string;
  genders: Gender[] = [];
  projects: Project[] = [];
  users: User[] = [];
  selectedUsers: User[] = [];
  selectedProjects: Project[] = [];

  constructor(private authService: AuthService,
    private userService: UserService,
    private lookUpService: LookUpService,
    private projectService: ProjectService) {
  }

  async ngOnInit() {
    this.genders = await firstValueFrom(this.lookUpService.getAllGenders());
    this.projects = await firstValueFrom(this.projectService.getAllProjects());
    this.users = await firstValueFrom(this.userService.getAllUsers());


    this.userService.getCurrentUser().subscribe(user => {
      this.user = user;
      this.selectedGender = this.genders[user.id].name;
      this.uploadUrl = environment.baseUrl + "/Resources/"+ user.profilePicture;
      this.fillSelectedProjects();
      this.fillSelectedUsers();
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

  fillSelectedUsers() {
    if(this.user.reportingLine != null) {
      this.selectedUsers = this.users.filter(val =>{
        return this.user.reportingLine.includes(val.id);
      });
    }
  }

  fillSelectedProjects() {
    if(this.user.currentProjects != null) {
      this.selectedProjects = this.projects.filter(val =>{ 
        return this.user.currentProjects.includes(val.id) 
      });
    }
  }
}