module Read
  ( readL
  , readI
  , lineSeparator
  , commaSeparator
  , intConverter
  ) where

import Data.List

readL :: (String -> [String]) -> (String -> a) -> FilePath -> IO [a]
readL separator converter = fmap (map converter) . fmap separator . readFile

readI :: (String -> a) -> FilePath -> IO a
readI converter = fmap converter . readFile

lineSeparator :: String -> [String]
lineSeparator = lines

commaSeparator :: String -> [String]
commaSeparator = filter (\a -> a /= ",") . groupBy (\a b -> a /= ',' && b /= ',')

intConverter :: String -> Int
intConverter = read
