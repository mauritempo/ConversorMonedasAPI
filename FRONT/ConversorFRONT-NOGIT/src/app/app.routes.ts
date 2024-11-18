import { Routes } from '@angular/router';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { soloLogueadoGuard } from './guards/solo-logueado.guard';
import { soloLogueadoPREMIUMGuard } from './guards/solo-logueado-premium.guard';
import { Component } from '@angular/core';
import { HomeComponent } from './pages/home/home.component';
import { HomeFreeComponent } from './pages/home-free/home-free.component';
import { RegisterComponent } from './pages/register/register.component';
import { soloPublicoGuard } from './guards/solo-publico.guard';
import { NotFundComponent } from './pages/not-fund/not-fund.component';
import { LoginComponent } from './pages/login/login.component';

export const routes: Routes = [
    {
        path: '',
        component: DashboardComponent,
        canActivate: [soloLogueadoGuard],
        children: [
            {
                path: "home-free",
                component: HomeFreeComponent,
                
            },
            {
                path: "home",
                component:HomeComponent,
                canActivate: [soloLogueadoPREMIUMGuard]
            },
            
        ]
    },
    {
        path: 'register',
        component: RegisterComponent,
        canActivate: [soloPublicoGuard]
    },
    {
        path: 'login',
        component: LoginComponent,
        canActivate: [soloPublicoGuard]
    },
    {
        path: "not-found",
        component: NotFundComponent
    },
    {
        path: "**",
        redirectTo: "not-found",
        pathMatch: "full",

    },
];
