import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import * as moment from 'moment';

import { environment } from '../../environments/environment';
import { User } from '../models/user';

@Injectable({ providedIn: 'root' })
export class AuthenticationService {
    private currentUserSubject: BehaviorSubject<User>;
    public currentUser: Observable<User>;

    expiresAt: number;
    userProfile: any;
    accessToken: string;
    authenticated: boolean;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User {
        return this.currentUserSubject.value;
    }

    login(username: string, password: string, keepMe: boolean) {
        return this.http.post<any>(`${environment.apiUrl}/users/authenticate`, { username, password, keepMe })
            .pipe(map(authResult => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                this.setSession(authResult);
                // TODO: should extract the user from authResult
                this.currentUserSubject.next(authResult.user);
                return true;
            }));
    }

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    private setSession(authResult){

      const expiresAt = moment().add(authResult.expiresIn,'second');

      localStorage.setItem('id_token', authResult.idToken);
      localStorage.setItem("expires_at", JSON.stringify(expiresAt.valueOf()) );
      localStorage.setItem('currentUser', JSON.stringify(authResult.user));
    }
    
    getExpiration() {
      const expiration = localStorage.getItem("expires_at");
      const expiresAt = JSON.parse(expiration);
      return moment(expiresAt);
  } 
}
