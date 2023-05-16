import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/UserService';
import { ActivatedRoute } from '@angular/router';
import { Role } from '../../models/Role';
import { RoleService } from '../../services/RoleService';
import { Project } from '../../models/Project';
import { ProjectService } from '../../services/ProjectService';
import Swal from 'sweetalert2';
import { environment } from 'src/environments/environment.prod';
import { LookUpService } from '../../services/LookupService';
import { firstValueFrom } from 'rxjs';
import { Gender } from '../../models/Gender';

@Component({
  selector: 'app-create-edit-user',
  templateUrl: './create-edit-user.component.html',
  styleUrls: ['./create-edit-user.component.less']
})
export class CreateEditUserComponent implements OnInit {
  roles: Role[] = [];
  projects: Project[] = [];
  selectedProjects: Project[] = [];
  genders: Gender[] = [];
  selectedUser: User = new User();
  users: User[] = [];
  userId! :number;
  selectedUsers: User[] = [];
  file: File;
  uploadUrl!: string;

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private roleService: RoleService,
    private lookupService: LookUpService,
    private projectService: ProjectService) { 
     
    }

  async ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    this.projects = await firstValueFrom(this.projectService.getAllProjects());
    this.users = await firstValueFrom(this.userService.getAllUsers());
    this.roles = await firstValueFrom(this.roleService.getAllRoles());
    this.genders = await firstValueFrom(this.lookupService.getAllGenders());

    if (id){
      this.userService.getUserById(Number(id)).subscribe(user => {
          this.userId = Number(id);
          this.selectedUser = user;
          this.uploadUrl = environment.baseUrl + "/Resources/"+ user.profilePicture;
          this.fillSelectedProjects();
          this.fillSelectedUsers();
      });
      return;
    }

    this.fillSelectedProjects();
    this.fillSelectedUsers();
  }

  fillSelectedProjects() {
    if(this.selectedUser.currentProjects != null) {
      this.selectedProjects = this.projects.filter(val =>{ 
        return this.selectedUser.currentProjects.includes(val.id) 
      });
    }
  }

  fillSelectedUsers() {
    if(this.selectedUser.reportingLine != null) {
      this.selectedUsers = this.users.filter(val =>{
        return this.selectedUser.reportingLine.includes(val.id) 
      });
    }
  }

  createOrUpdateUser() {
    this.selectedUser.currentProjects = this.selectedProjects.map(val => val.id);
    this.selectedUser.reportingLine = this.selectedUsers.map(val => val.id);

    if (this.userId) {
      this.userService.updateUser(this.selectedUser).subscribe(user => {
        Swal.fire(
          'User is updated',
          '',
          'success'
        );
      });
    } else {
      this.userService.createUser(this.selectedUser).subscribe(user => {
        Swal.fire(
          'User is created',
          '',
          'success'
        );
        this.selectedUser = new User();
      });
    }
    
  }

  updateUser() {
    this.userService.updateUser(this.selectedUser).subscribe(user => {
      this.selectedUser = new User();
    });
  }

  onFilechange(event: any) {
    this.file = event.target.files[0];
    if (this.file) {
      this.userService.uploadById(this.userId ,this.file).subscribe(resp => {
        this.uploadUrl = environment.baseUrl + "/Resources/"+ resp.fileName;
        this.selectedUser.profilePicture = resp.fileName;
        event.target.value = "";
      });
    }
    else
    {
      alert("Please select a file first")
    }
  }

}
