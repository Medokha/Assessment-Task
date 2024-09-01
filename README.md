Task sql to select top three we use ranking function use ROW_NUMBER

WITH RankedEmployees AS (
    SELECT 
        EmployeeID,
        Name,
        Department,
        Salary,
        ROW_NUMBER() OVER (PARTITION BY Department ORDER BY Salary DESC, EmployeeID ASC) AS Rank
    FROM 
        Employees
)
SELECT 
    EmployeeID,
    Name,
    Department,
    Salary
FROM 
    RankedEmployees
WHERE 
    Rank <= 3
ORDER BY 
    Department, Salary DESC;
