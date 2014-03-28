Create table Product 
(
  Id bigint primary key identity(1,1),
  Name varchar(256),
  Price decimal(9,2),
  Qty Int
   
)

------------------------------------
------------------------------------

; WITH
t1 AS (SELECT 1 N UNION ALL SELECT 1 N),    -- 2            ,              2 raise to power 1
t2 AS (SELECT 1 N FROM t1 x, t1 y),            -- 8            ,   2 raise to power 3
t3 AS (SELECT 1 N FROM t2 x, t2 y),            -- 128         ,   2 raise to power 7
t4 AS (SELECT 1 N FROM t3 x, t3 y),            -- 32768        , 2 raise to power 15
t5 AS (SELECT 1 N FROM t4 x, t4 y),           -- 2147483648 , 2 raise to power 31
tally as
(
                   SELECT
                             ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) N                      
                   FROM
                             t1 , t2 , t3, t4 , t5
                             -- you can delete or add ts here to increase decrease the rows, or
)
 
insert into Product ( Name, Price, Qty)
select 
	'P ' + Convert(varchar(8), n ) ,
	
	
Convert(decimal(9,2),	RAND( (DATEPART(mm, GETDATE()) * 100000 )
+ (DATEPART(ss, GETDATE()) * 1000 )
+ DATEPART(ms, GETDATE()) ) *  (n % 9)  * 1000
)
,
Convert(int,
RAND( (DATEPART(mm, GETDATE()) * 100000 )
+ (DATEPART(ss, GETDATE()) * 1000 )
+ DATEPART(ms, GETDATE()) ) * 10 * (n % 9) * (n % 9))


from tally where n <= 1000000