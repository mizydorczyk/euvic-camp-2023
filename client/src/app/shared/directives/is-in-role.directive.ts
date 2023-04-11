import {Directive, Input, OnDestroy, OnInit, TemplateRef, ViewContainerRef} from '@angular/core';
import {Subscription} from "rxjs";
import {AccountService} from "../../services/account.service";
import {User} from "../../../models/user";

@Directive({
  selector: '[isInRole]'
})
export class IsInRoleDirective implements OnInit, OnDestroy {
  @Input() isInRole: string = 'User';
  private subscription: Subscription = new Subscription();

  constructor(private viewContainerRef: ViewContainerRef, private templateRef: TemplateRef<any>, private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.subscription = this.accountService.currentUser$.subscribe({
      next: user => {
        if (user && user.roles.indexOf(this.isInRole) > -1) {
          this.viewContainerRef.createEmbeddedView(this.templateRef);
        } else {
          this.viewContainerRef.clear()
        }
      }
    })
  };

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
