import Read (readL, lineSeparator, intConverter)

fuelReq :: Int -> Int
fuelReq a = div a 3 - 2

p1 :: [Int] -> Int
p1 = sum . map fuelReq 

fuelReqRec :: Int -> Int
fuelReqRec a = let b = fuelReq a in if b < 0 then 0 else b + fuelReqRec b

p2 :: [Int] -> Int
p2 = sum . map fuelReqRec

main :: IO()
main = do
  masses <- readL lineSeparator intConverter "1.txt"
  print . p1 $ masses
  print . p2 $ masses
