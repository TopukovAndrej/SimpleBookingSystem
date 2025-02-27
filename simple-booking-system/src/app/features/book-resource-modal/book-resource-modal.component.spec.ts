import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BookResourceModalComponent } from './book-resource-modal.component';

describe('BookResourceModalComponent', () => {
  let component: BookResourceModalComponent;
  let fixture: ComponentFixture<BookResourceModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BookResourceModalComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BookResourceModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
