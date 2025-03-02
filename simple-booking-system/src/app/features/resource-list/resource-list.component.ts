import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { IResource } from '../../interfaces/resource.interface';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { BookResourceModalComponent } from '../book-resource-modal/book-resource-modal.component';

@Component({
  selector: 'app-resource-list',
  imports: [MatTableModule, MatButtonModule],
  templateUrl: './resource-list.component.html',
  styleUrl: './resource-list.component.css',
})
export class ResourceListComponent {
  public dummyResourceData: IResource[] = [
    {
      id: 1,
      name: 'Resource 1',
      quantity: 0,
    },
    {
      id: 2,
      name: 'Resource 2',
      quantity: 5,
    },
    {
      id: 3,
      name: 'Resource 3',
      quantity: 6,
    },
    {
      id: 4,
      name: 'Resource 4',
      quantity: 7,
    },
    {
      id: 5,
      name: 'Resource 5',
      quantity: 8,
    },
  ];

  public columnNames: string[] = ['id', 'name', 'book'];

  constructor(private readonly dialog: MatDialog) {}

  public openBookResourceModal(resourceName: string): void {
    const dialogRef: MatDialogRef<BookResourceModalComponent> =
      this.dialog.open(BookResourceModalComponent, {
        data: { resourceName: resourceName },
      });
  }
}
