# Adding Spree to existing Rails App

<h3>Gemfile</h3>


Use the stable build, <i>bootstrap</i> for the frontend, and the <i>devise</i> authentication gem.
```ruby
gem 'spree', github: 'spree/spree', branch: '2-4-stable'
gem 'spree_bootstrap_frontend', github: '200Creative/spree_bootstrap_frontend'
gem 'spree_auth_devise', github: 'spree/spree_auth_devise', branch: '2-4-stable'
```
Optionally, if you need volume pricing add
```ruby
gem 'spree_volume_pricing', github: 'spree-contrib/spree_volume_pricing'
```
AT this point, check your version of rails, otherwise there may be conflicts.
```ruby
gem 'rails', '4.1.8'
```
Then run bundle install.

<h3>Install Spree</h3>
```
rails g spree:install
```
Reload your root webpage and you should now see the Spree frontend site with products.  
It will be a bit ugly, but nothing some bootstrap css can't fix!
<h3>Install Bootstrap</h3>
```
rails g spree_bootstrap_frontend:install
```
Change the <i>application.css</i> to file to scss and add the following. This will reference the frontend css, as well as allow us to use the <i>font-awesome</i> gem that comes with the <i>Spree</i> package (if we don't already have it).
```ruby
@import "spree/frontend/bootstrap_frontend";
@import "font-awesome";
```
<h3>Login to admin and create a new Product</h3>
Navigate to the backend using <i>/admin</i> and you be asked to login (use the email you specified in the install eg. spree@example.com, and password).  You will see the admin area where you can go to the products -> +New Product, where you can add a product name, price, image etc.  Then navigate back to home menu.

<h3>Change the Routing</h3>
Mount <i>Spree</i> at <i>/shop</i>.  Note the comment from the <i>Spree</i> developers.

```ruby
# We ask that you don't use the :as option here, as Spree relies on it being the default of "spree"
mount Spree::Core::Engine, :at => '/shop'
```
<h3>Add link in index file</h3>
<h3>Using the <i>Deface</i> Library to Customise</h3>
From <i>Spree</i> docs...
<blockquote>"Deface is a standalone Rails library that enables you to customize Erb templates without needing to directly edit the underlying view file. Deface allows you to use standard CSS3 style selectors to target any element (including Ruby blocks), and perform an action against all the matching elements"</blockquote>
<ul>
<li>change logo</li>
<li>remove search bar</li>
<li>add menu items</li>
</ul>
<h4>Use the Spree Controller Helpers</h4>
In <i>applicationHelper.rb</i> (or whereever you feel appropriate) 
add some methods that can be used in the erb text being included in the menu items.
```ruby
include Spree::Core::ControllerHelpers

  def is_admin?
    if spree_current_user
      spree_current_user.has_spree_role?('admin')
    end
  end

  def require_login
    if spree_current_user
      unless spree_current_user.has_spree_role?('admin')
        redirect_to spree_login_path
      end
    else
      redirect_to spree_login_path
    end
  end
```






