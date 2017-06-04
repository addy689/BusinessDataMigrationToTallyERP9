# PamJoeHandcraftedAppSuite
.NET application suite for managing Pam &amp; Joe Handcrafted's business processes

Modules

A. Bulk Upload of inventory products to Shopify Webstore
  
  1. [complete] Upload inventory images to Flickr for temporary hosting using the Flickr.NET API (https://www.nuget.org/packages/FlickrNet | https://github.com/samjudson/flickr-net)
  2. [in progress] Parse input inventory data (CSV), map each inventory item to it's Flickr photo URL, and finally convert to Shopify data objects
  3. [in progress] Use the Shopify API to upload products into the Shop using the data created in 2.

B. Inventory PriceTags Generator

1. [complete] Create a richly-formatted excel sheet with small printable price (and inventory code) tags for input inventory data (in CSV). Uses EPPlus.Core (https://www.nuget.org/packages/EPPlus.Core) and CsvHelper (https://www.nuget.org/packages/CsvHelper)

C. [complete] Migrating business data from CSV data sources to proprietary TallyERP9 software.
