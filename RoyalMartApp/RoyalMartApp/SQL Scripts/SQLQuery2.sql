/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [order_detailsID]
      ,[invoice_id]
      ,[item_name]
      ,[unit_price]
      ,[discount_peritem]
      ,[quantity]
      ,[subtotal]
      ,[tax]
      ,[totalcost]
  FROM [RoyalMartDB].[dbo].[order_details]