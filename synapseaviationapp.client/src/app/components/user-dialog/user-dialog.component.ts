import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserDataService } from '../../../services/user-data.service';
import { ToastrService } from 'ngx-toastr';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-user-dialog',
  templateUrl: './user-dialog.component.html',
  styleUrls: ['./user-dialog.component.css']
})
export class UserDialogComponent {
  userForm: FormGroup;
  user: any;
  constructor(
    public dialogRef: MatDialogRef<UserDialogComponent>, @Inject(MAT_DIALOG_DATA) public userData: any, private userDataService: UserDataService, private toastr: ToastrService, private formBuilder: FormBuilder) {
    this.user = { ...userData.user };
    this.userForm = this.formBuilder.group({
      firstName: [this.user.firstName, Validators.required],
      lastName: [this.user.lastName, Validators.required],
      age: [this.user.age, [Validators.required, Validators.pattern(/^\d{1,3}$/), Validators.min(0), Validators.max(120)], this.ageRangeValidator()]
    });
  }

  ageRangeValidator() {
    return (control: AbstractControl): Promise<{ [key: string]: boolean } | null> => {
      return Promise.resolve(this.validateAge(control));
    };
  }

  validateAge(control: AbstractControl): { [key: string]: boolean } | null {
    const age = control.value;
    if (age !== null && age > 120) {
      return { 'maxAgeExceeded': true };
    }
    return null;
  }

  onSave(): void {
    if (this.userForm.invalid) {
      return;
    }
    this.userDataService.updateUser(this.user).subscribe(
      updatedUser => {
        this.dialogRef.close(updatedUser);
        this.toastr.success('Saved successfully!', 'Success', {
          timeOut: 3000,
          progressBar: true,
          closeButton: true,
          tapToDismiss: false,
          toastClass: 'toast-custom-success toast-show'
        });
      },
      error => {
        console.error('Error updating user:', error);
      }
    );
  }

  onCancel(): void {
    this.dialogRef.close();
  }

}





