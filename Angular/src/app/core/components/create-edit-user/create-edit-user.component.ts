import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/UserService';
import { ActivatedRoute } from '@angular/router';
import { Role } from '../../models/Role';
import { RoleService } from '../../services/RoleService';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Project } from '../../models/Project';
import { ProjectService } from '../../services/ProjectService';
import Swal from 'sweetalert2';
import { environment } from 'src/environments/environment.prod';

@Component({
  selector: 'app-create-edit-user',
  templateUrl: './create-edit-user.component.html',
  styleUrls: ['./create-edit-user.component.less']
})
export class CreateEditUserComponent implements OnInit {
  roles: Role[] = [];
  projects: Project[] = [];
  selected: Project[] = [];
  selectedUser: User = new User();
  users: User[] = [];
  userId! :number;
  selectedUsers: User[] = [];
  file: File;
  uploadUrl!: string;

  constructor(private userService: UserService,
    private route: ActivatedRoute,
    private roleService: RoleService,
    private projectService: ProjectService) { 
     
    }

  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if(id){
      this.userService.getUserById(Number(id)).subscribe(user => {
        this.selectedUser = user;
        this.userId = Number(id);
        this.projectService.getAllProjects().subscribe(projects => {
          this.projects = projects;
          this.uploadUrl = environment.baseUrl +'/Resources/'+ user.profilePicture;
          this.fillSelectedProjects();
        });
        this.userService.getAllUsers().subscribe(users => {
          this.users = users;
          this.fillSelectedUsers();
        });
      });
    }
    else
    {
      this.projectService.getAllProjects().subscribe(projects => {
        this.projects = projects;
      });
      this.userService.getAllUsers().subscribe(users => {
        this.users = users;
        this.fillSelectedUsers();
      });
    }

    this.roleService.getAllRoles().subscribe(roles=> {
      this.roles = roles;
    });
  }

  fillSelectedProjects() {
    if(this.selectedUser.currentProjects != null) {
      this.selected = this.projects.filter(val =>{ return !this.selectedUser.currentProjects.includes(val.id) });
    }
  }

  fillSelectedUsers() {
    if(this.selectedUser.reportingLine != null) {
      this.selectedUsers = this.users.filter(val =>{
        return !this.selectedUser.reportingLine.includes(val.id) 
      });
    }
  }

  createOrUpdateUser() {
    this.selectedUser.currentProjects = this.selected.map(val => val.id);

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
        this.selectedUser.profilePicture = this.uploadUrl;
        event.target.value = "";
      });
    }
    else
    {
      alert("Please select a file first")
    }
  }

}
