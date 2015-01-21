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





