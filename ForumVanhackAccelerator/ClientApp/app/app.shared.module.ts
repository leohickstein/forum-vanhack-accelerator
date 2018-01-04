import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AuthInterceptor } from './services/auth.interceptor';
import { AuthService } from './services/auth.service';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { HomeComponent } from './components/home/home.component';

import { TopicListComponent } from './components/topic/topic-list.component';
import { TopicDetailComponent } from './components/topic/topic-detail.component';
import { TopicEditComponent } from './components/topic/topic-edit.component';
import { PostListComponent } from './components/post/post-list.component';
import { LoginComponent } from './components/login/login.component';


@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        TopicListComponent,
        TopicDetailComponent,
        TopicEditComponent,
        PostListComponent,
        LoginComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'forum', pathMatch: 'full' },
            { path: 'forum', component: HomeComponent },
            { path: 'forum/:filter', component: HomeComponent },
            { path: 'topic/create', component: TopicEditComponent },
            { path: 'topic/:id', component: TopicDetailComponent },            
            { path: 'topic/edit/:id', component: TopicEditComponent },
            { path: 'login', component: LoginComponent },
            { path: '**', redirectTo: 'forum' }
        ])
    ],
    providers: [
        AuthService,
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true
        }
    ]
})
export class AppModuleShared {
}
