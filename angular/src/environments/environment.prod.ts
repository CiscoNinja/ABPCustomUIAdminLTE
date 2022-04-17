import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'CustomUIAdminLTE',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44384',
    redirectUri: baseUrl,
    clientId: 'CustomUIAdminLTE_App',
    responseType: 'code',
    scope: 'offline_access CustomUIAdminLTE',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44384',
      rootNamespace: 'CustomUIAdminLTE',
    },
  },
} as Environment;
