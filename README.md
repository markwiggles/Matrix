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
Change the application.css file to scss and add the following.
```ruby
@import "spree/frontend/bootstrap_frontend";
@import "font-awesome";
```

