import { Component, OnInit } from '@angular/core';
import { UserDataService } from '../../../services/user-data.service';
import { MatDialog } from '@angular/material/dialog';
import { UserDialogComponent } from '../user-dialog/user-dialog.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements OnInit {
  users: any[] = [];
  filteredUsers: any[] = [];
  sortDirection: string = 'asc';

  constructor(private userDataService: UserDataService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userDataService.getAllUsers().subscribe((users) => {
      this.users = users;
      this.filteredUsers = this.users;
    });
  }

  applyFilter(searchTerm: string): void {
    this.filteredUsers = this.users.filter(user =>
      user.firstName.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }

  openDialog(user: any): void {
    const dialogRef = this.dialog.open(UserDialogComponent, {
      width: '400px',
      data: { user }
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        // Update the data if changes are saved
        const index = this.users.findIndex(u => u.id === result.id);
        if (index !== -1) {
          this.users[index] = result;
          this.filteredUsers = [...this.users];
        }
      }
    });
  }

  sortByFirstName(): void {
    this.filteredUsers.sort((a, b) => {
      const nameA = a.firstName.toLowerCase();
      const nameB = b.firstName.toLowerCase();
      let comparison = 0;

      if (this.sortDirection === 'asc') {
        comparison = nameA.localeCompare(nameB);
      } else {
        comparison = nameB.localeCompare(nameA);
      }
      return comparison;
    });
    this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
  }
}
