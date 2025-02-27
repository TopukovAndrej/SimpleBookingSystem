import { ApplicationConfig } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';

export const coreConfig: ApplicationConfig = {
  providers: [provideHttpClient()],
};
