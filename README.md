# PamJoeHandcraftedAppSuite
.NET application suite for managing Pam &amp; Joe Handcrafted's business processes

Modules

1. Online WebStore
  a. [complete] Uploading inventory images to Flickr for temporary hosting using the Flickr.NET API (https://www.nuget.org/packages/FlickrNet | https://github.com/samjudson/flickr-net)
  b. [in progress] Parse inventory CSV data source, map each inventory item to it's Flickr photo URL, and finally convert to Shopify data objects
  c. [in progress] Use the Shopify API to upload products into the Shop using the data created in b.

2. PriceTagsGenerator
  Creates a richly-formatted excel sheet with small printable price (and inventory code) tags for input inventory data (in CSV). Uses EPPlus.Core (https://www.nuget.org/packages/EPPlus.Core) and CsvHelper (https://www.nuget.org/packages/CsvHelper)

3. [complete] Migrating business data from CSV data sources to proprietary TallyERP9 software.
