import { Component, OnInit } from '@angular/core';
import { User } from '../../models/User';
import { UserService } from '../../services/UserService';

@Component({
  selector: 'app-create-edit-user',
  templateUrl: './create-edit-user.component.html',
  styleUrls: ['./create-edit-user.component.less']
})
export class CreateEditUserComponent implements OnInit {
  users: User[] = [];
  roles: string[] = ['Manager', 'Employee'];
  projects: string[] = ['Project A', 'Project B', 'Project C'];
  selectedUser: User = new User();
  selectedProject: number = 0;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getAllUsers().subscribe(users => this.users = users);
  }

  createUser() {
    this.userService.createUser(this.selectedUser).subscribe(user => {
      this.users.push(user);
      this.selectedUser = new User();
    });
  }

  updateUser() {
    this.userService.updateUser(this.selectedUser).subscribe(user => {
      const index = this.users.findIndex(u => u.id === user.id);
      this.users[index] = user;
      this.selectedUser = new User();
    });
  }

  deleteUser(user: User) {
    this.userService.deleteUser(user).subscribe(() => {
      const index = this.users.findIndex(u => u.id === user.id);
      this.users.splice(index, 1);
    });
  }
}
