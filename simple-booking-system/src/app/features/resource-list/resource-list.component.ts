import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { IResourceDto } from '../../interfaces/resource.interface';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { BookResourceModalComponent } from '../book-resource-modal/book-resource-modal.component';
import { HttpService } from '../../core';
import { of, Subscription, switchMap, take } from 'rxjs';
import { IBookResourceRequest } from '../../interfaces/book-resource-request.interface';
import { DialogActions } from '../../models/dialog-actions';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';

@Component({
  selector: 'app-resource-list',
  imports: [MatTableModule, MatButtonModule, MatSnackBarModule],
  templateUrl: './resource-list.component.html',
  styleUrl: './resource-list.component.css',
})
export class ResourceListComponent implements OnInit, OnDestroy {
  public resources: IResourceDto[] = [];

  public columnNames: string[] = ['id', 'name', 'book'];

  private fetchResources$: Subscription | undefined;

  private bookResourceModalSubcription$: Subscription | undefined;

  constructor(
    private readonly httpService: HttpService,
    private readonly dialog: MatDialog,
    private readonly snackBar: MatSnackBar
  ) {}

  public ngOnInit(): void {
    this.fetchResources$ = this.httpService
      .getAllResources()
      .pipe(take(1))
      .subscribe((resources: IResourceDto[]) => {
        this.resources = resources;
      });
  }

  public ngOnDestroy(): void {
    this.fetchResources$?.unsubscribe();
    this.bookResourceModalSubcription$?.unsubscribe();
  }

  public openBookResourceModal(resourceId: number, resourceName: string): void {
    const dialogRef: MatDialogRef<BookResourceModalComponent> =
      this.dialog.open(BookResourceModalComponent, {
        data: { resourceId: resourceId, resourceName: resourceName },
      });

    this.bookResourceModalSubcription$ = dialogRef
      .afterClosed()
      .pipe(
        take(1),
        switchMap(
          (
            dialogResult:
              | {
                  action: string;
                  data: IBookResourceRequest | null;
                }
              | undefined
          ) => {
            if (
              dialogResult?.action === DialogActions.DialogActionBook &&
              dialogResult?.data
            ) {
              return this.httpService.bookResource(dialogResult?.data);
            }

            return of(undefined);
          }
        )
      )
      .subscribe({
        next: (response: void | undefined) => {
          if (response === undefined) {
            return;
          }

          this.showMessage('Booking completed successfully!', 'Success');
        },
        error: (error) => {
          this.showMessage(error.error, 'Failed');
        },
      });
  }

  private showMessage(
    message: string,
    action: string,
    duration: number = 4000
  ) {
    this.snackBar.open(message, action, {
      duration: duration,
      horizontalPosition: 'center',
      verticalPosition: 'top',
    });
  }
}
