import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { IResourceDto } from '../../shared/interfaces/resource.interface';
import { MatButtonModule } from '@angular/material/button';
import { HttpService } from '../../core';
import { Subscription, take } from 'rxjs';

@Component({
  selector: 'app-resource-list',
  imports: [MatTableModule, MatButtonModule],
  templateUrl: './resource-list.component.html',
  styleUrl: './resource-list.component.css',
})
export class ResourceListComponent implements OnInit, OnDestroy {
  public resources: IResourceDto[] = [];

  public columnNames: string[] = ['id', 'name', 'book'];

  private fetchResources$: Subscription | undefined;

  constructor(private readonly httpService: HttpService) {}

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
  }

  public bookResource(resource: IResourceDto): void {
    console.log('I can book');
  }
}
