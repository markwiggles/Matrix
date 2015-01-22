# Example: add <i>Spree</i> to existing Rails App

[Install Spree](#install-spree)

[Install Bootstrap](#install-bootstrap)

[Create New product](#create-new-product)

[Add Routes](#add-routes)

[Customise with Deface](#customise-with-deface)

----
<h3>Gemfile</h3> 

Add the <i>Spree</i> stable build, the <i>bootstrap</i> gem for the frontend, as well as the <i>devise</i> authentication gem.

```ruby
gem 'spree', github: 'spree/spree', branch: '2-4-stable'
gem 'spree_bootstrap_frontend', github: '200Creative/spree_bootstrap_frontend'
gem 'spree_auth_devise', github: 'spree/spree_auth_devise', branch: '2-4-stable'
```
Optionally, if you need volume pricing add
```ruby
gem 'spree_volume_pricing', github: 'spree-contrib/spree_volume_pricing'
```
At this point, check your version of rails, otherwise there may be conflicts.

```ruby
gem 'rails', '4.1.8'
```
Then run bundle install.

<h3>Install Spree</h3>

Unless, you have used <i>Spree</i> before, it is a good idea to load your first project with the sample data as it will give you the opportunity to explore the setup including configuration, taxons, freight etc. You can pass additional commands to give you the bare bones app as  <i> --sample --seed </i>.  See the Spree Github for more info.  Otherwise, for a complete install...

```
rails g spree:install

```

Reload your root webpage and you should now see the <i>Spree</i> frontend site with products.  
It will be a bit ugly, but nothing some <i>bootstrap</i> css can't fix!

<h3>Install Bootstrap</h3>
```
rails g spree_bootstrap_frontend:install
```
Note: on refresh, you may get an error message re: additional assets. If so then add required files to the <i>assets.rb</i> file as done here, and restart the server.
```ruby
# Precompile additional assets.
# application.js, application.css, and all non-JS/CSS in app/assets folder are already added.
Rails.application.config.assets.precompile += %w( favicon.ico spree_header.jpg logo/spree_50.png)
```

Spree also uses the <i>font-awesome</i> gem which can be helpful in your markup. To use this, you will need to change the <i>application.css</i> to file to .scss and add the following. 
```ruby
@import "font-awesome";
```
<h3>Create New Product</h3>
Navigate to the backend using <i>/admin</i> and you be asked to login (use the email you specified in the install eg. spree@example.com, and password).  You will see the admin area where you can go to the products -> +New Product, where you can add a product name, price, image etc.  Then navigate back to home menu.

<h3>Add Routes</h3>
In <i>routes.rb</i> - mount <i>Spree</i> at <i>/shop</i>.  Note the comment from the <i>Spree</i> developers.

```ruby
# We ask that you don't use the :as option here, as Spree relies on it being the default of "spree"
mount Spree::Core::Engine, :at => '/shop'
```
Next, add a route which will navigate direct to the product that you want to sell. 
For this you will need to get the integer id for newly created product (here it is 17), from your database table.
```
  Spree::Core::Engine.routes.draw do
    root :to => 'products#show', as: 'buy_product', :id => 17
  end
```
The original webpage will now return as the root page, and you will be able to navigate to the <i>Spree</i> site using  <i>/shop</i> or in you code as <i>buy_product</i> path.

<h3>Customise with <i>Deface</i></h3>
From <i>Spree</i> docs...
<blockquote>"Deface is a standalone Rails library that enables you to customize Erb templates without needing to directly edit the underlying view file. Deface allows you to use standard CSS3 style selectors to target any element (including Ruby blocks), and perform an action against all the matching elements"</blockquote>

Using <i>Deface</i>, we will change a few things with as little code as possible.
<ul>
<li>Change logo</li>
<li>Remove search bar</li>
<li>Add favicon</li>
<li>Add menu items</li>
<li>Add payment details (credit card icons)
</ul>
<h4>Use the Spree Controller Helpers</h4>
To help manage the authentication, we add two helper methods (in <i>applicationHelper.rb</i>, or where ever you feel appropriate:
<ol>
<li><i>is_admin?</i> - this can be used as <i>erb</i> text passed as a parameter in the <i>Deface override</i> file.</li> <li><i>require_login</i> - can be used as a before_filter (in controllers) for pages in the main website which will need authorisation i.e. using the devise authentication provided with the Spree Application.</li>
</ol>

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
