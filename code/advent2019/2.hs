import Read (readL, commaSeparator, intConverter)

save :: Int -> Int -> [Int] -> [Int]
save value position program = take position program ++ [value] ++ drop (position+1) program

add :: Int -> Int -> Int -> [Int] -> [Int]
add a b c program = save ((program !! a) + (program !! b)) c program

mult :: Int -> Int -> Int -> [Int] -> [Int]
mult a b c program = save ((program !! a) * (program !! b)) c program

subexecute :: (Int -> Int -> Int -> [Int] -> [Int]) -> Int -> [Int] -> [Int]
subexecute func position program = func (program !! (position+1)) (program !! (position+2)) (program !! (position+3)) program

execute :: Int -> [Int] -> [Int]
execute position program = case program !! position of
  1 -> execute (position+4) $ subexecute add position program
  2 -> execute (position+4) $ subexecute mult position program
  99 -> program
  _ -> []

p1 :: [Int] -> Int
p1 = head . execute 0 . save 12 1 . save 2 2

executeWithNounAndVerb :: Int -> Int -> [Int] -> [Int]
executeWithNounAndVerb noun verb = execute 0 . save noun 1 . save verb 2

p2 :: [Int] -> Int
p2 program = let (noun,verb,_) = head . dropWhile (\(_,_,o) -> o /= 19690720) . 
                   concatMap (\n -> map (\v -> (n,v,head . executeWithNounAndVerb n v $ program)) [0..99]) $ [0..99]
             in 100 * noun + verb

main :: IO()
main = do
  program <- readL commaSeparator intConverter "2.txt"
  print . p1 $ program
  print . p2 $ program
