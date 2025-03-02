import { CommonModule } from '@angular/common';
import { Component, Inject, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import {
  MAT_DIALOG_DATA,
  MatDialogModule,
  MatDialogRef,
} from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-book-resource-modal',
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    ReactiveFormsModule,
    CommonModule,
  ],
  templateUrl: './book-resource-modal.component.html',
  styleUrl: './book-resource-modal.component.css',
})
export class BookResourceModalComponent implements OnInit {
  public bookResourceForm!: FormGroup;
  public modalHeader: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: any,
    private readonly dialogRef: MatDialogRef<BookResourceModalComponent>,
    private readonly formBuilder: FormBuilder
  ) {
    this.modalHeader = this.data.resourceName;
  }

  public closeModal(): void {
    this.dialogRef.close();
  }

  public ngOnInit(): void {
    this.initForm();
  }

  private initForm(): void {
    this.bookResourceForm = this.formBuilder.group({
      fromDate: ['', Validators.required],
      toDate: ['', Validators.required],
      quantity: [
        '',
        Validators.required,
        Validators.min(0),
        Validators.max(100),
      ],
    });
  }
}
