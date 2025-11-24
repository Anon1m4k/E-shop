-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Ноя 21 2025 г., 06:57
-- Версия сервера: 5.7.25
-- Версия PHP: 7.1.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `internet_magazine_`
--

-- --------------------------------------------------------

--
-- Структура таблицы `discount_card`
--

CREATE TABLE `discount_card` (
  `Discount_Card_Number` int(20) NOT NULL,
  `Owner_FIO` varchar(100) DEFAULT NULL,
  `Phone` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Discount_percentage` decimal(3,2) DEFAULT NULL,
  `Active` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `invoice`
--

CREATE TABLE `invoice` (
  `ID_Invoice` int(11) NOT NULL,
  `SerialNumber` varchar(50) NOT NULL,
  `Date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `invoice`
--

INSERT INTO `invoice` (`ID_Invoice`, `SerialNumber`, `Date`) VALUES
(1, '6епрп', '2025-11-11 08:23:39'),
(2, 'ABCD425_123*', '2025-11-11 08:42:29'),
(4, 'Amvg33', '2025-11-11 10:58:08'),
(5, 'GIM33', '2025-11-18 08:31:34'),
(6, 'длщдлщдлб', '2025-11-11 08:42:29');

-- --------------------------------------------------------

--
-- Структура таблицы `invoiceitems`
--

CREATE TABLE `invoiceitems` (
  `ID_InvoiceItem` int(11) NOT NULL,
  `ID_Invoice` int(11) NOT NULL,
  `Article` varchar(255) NOT NULL,
  `Name` varchar(500) NOT NULL,
  `Category` varchar(50) NOT NULL,
  `Quantity` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Unit` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `invoiceitems`
--

INSERT INTO `invoiceitems` (`ID_InvoiceItem`, `ID_Invoice`, `Article`, `Name`, `Category`, `Quantity`, `Price`, `Unit`) VALUES
(8, 5, '10', 'Монитор', 'Техника', 6, '15000.00', 'шт'),
(17, 1, '3', 'Компьютер', 'Техника', 2, '555.00', 'шт'),
(22, 2, '123', 'Молоток отечественный', 'Молотки', 10, '100.00', 'шт'),
(23, 2, 'BE425 0', 'Топор лесной', 'Топоры', 2000, '500.00', 'грамм'),
(32, 4, 'щ', 'шщ', 'шщшщшщш', 555, '8888.00', 'м'),
(33, 4, 'щшщшщ', 'пппп', 'щш', 100, '55.00', 'л'),
(34, 6, '123EB', 'Молоток отечественный', 'Молотки', 88, '100.00', 'шт'),
(35, 6, 'BE425 05', 'Топор лесной', 'Топоры', 2000, '500.00', 'грамм');

-- --------------------------------------------------------

--
-- Структура таблицы `movement_of_goods`
--

CREATE TABLE `movement_of_goods` (
  `ID_Movement` int(11) NOT NULL,
  `Product_article` varchar(255) DEFAULT NULL,
  `Operation_type` enum('Приход','Расход') DEFAULT NULL,
  `Quantity` int(50) DEFAULT NULL,
  `Unit` varchar(10) DEFAULT NULL,
  `Data` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `product`
--

CREATE TABLE `product` (
  `Article` varchar(255) NOT NULL,
  `Name` varchar(500) DEFAULT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  `Stock` int(50) DEFAULT NULL,
  `Unit` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `product`
--

INSERT INTO `product` (`Article`, `Name`, `Category`, `Price`, `Stock`, `Unit`) VALUES
('/11', 'лолдллд', 'ппгнг', '1000.00', 1, 'шт'),
('10', 'Монитор', 'Техника', '15000.00', 12, 'шт'),
('123', 'Молоток отечественный', 'Молотки', '100.00', 0, 'шт'),
('123E', 'Молоток отечественный', 'Молотки', '100.00', 0, 'шт'),
('123EB', 'Молоток отечественный', 'Молотки', '100.00', 20, 'шт'),
('2', 'Компьютер', 'Техника', '15000.00', 6, 'шт'),
('3', 'Мышь', 'Техника', '500.00', 10, 'шт'),
('BE425 0', 'Топор лесной', 'Топоры', '500.00', 0, 'грамм'),
('BE425 05', 'Топор лесной', 'Топоры', '500.00', 4000, 'грамм'),
('щ', 'шщ', 'шщшщшщш', '10.00', 0, 'м'),
('щшщшщ', 'шщшщшщшщ', 'щш', '55.00', 200, 'л'),
('щшщшщш', 'шщ', 'шщшщшщш', '10.00', 20, 'м');

-- --------------------------------------------------------

--
-- Структура таблицы `sale`
--

CREATE TABLE `sale` (
  `ID_Sales` int(11) NOT NULL,
  `Date_Sales` datetime DEFAULT NULL,
  `Product_article` varchar(255) DEFAULT NULL,
  `Quantity` int(50) DEFAULT NULL,
  `Price_per_Unit` decimal(10,2) DEFAULT NULL,
  `Discount_Card_Number` int(20) DEFAULT NULL,
  `Discount_amount` decimal(10,2) DEFAULT NULL,
  `Total_sum` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `discount_card`
--
ALTER TABLE `discount_card`
  ADD PRIMARY KEY (`Discount_Card_Number`);

--
-- Индексы таблицы `invoice`
--
ALTER TABLE `invoice`
  ADD PRIMARY KEY (`ID_Invoice`);

--
-- Индексы таблицы `invoiceitems`
--
ALTER TABLE `invoiceitems`
  ADD PRIMARY KEY (`ID_InvoiceItem`),
  ADD KEY `ID_Invoice` (`ID_Invoice`),
  ADD KEY `Article` (`Article`);

--
-- Индексы таблицы `movement_of_goods`
--
ALTER TABLE `movement_of_goods`
  ADD PRIMARY KEY (`ID_Movement`),
  ADD KEY `Product_article` (`Product_article`);

--
-- Индексы таблицы `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`Article`);

--
-- Индексы таблицы `sale`
--
ALTER TABLE `sale`
  ADD PRIMARY KEY (`ID_Sales`),
  ADD KEY `Product_article` (`Product_article`),
  ADD KEY `Discount_Card_Number` (`Discount_Card_Number`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `invoice`
--
ALTER TABLE `invoice`
  MODIFY `ID_Invoice` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT для таблицы `invoiceitems`
--
ALTER TABLE `invoiceitems`
  MODIFY `ID_InvoiceItem` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=36;

--
-- AUTO_INCREMENT для таблицы `movement_of_goods`
--
ALTER TABLE `movement_of_goods`
  MODIFY `ID_Movement` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `sale`
--
ALTER TABLE `sale`
  MODIFY `ID_Sales` int(11) NOT NULL AUTO_INCREMENT;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `invoiceitems`
--
ALTER TABLE `invoiceitems`
  ADD CONSTRAINT `invoiceitems_ibfk_1` FOREIGN KEY (`ID_Invoice`) REFERENCES `invoice` (`ID_Invoice`) ON DELETE CASCADE,
  ADD CONSTRAINT `invoiceitems_ibfk_2` FOREIGN KEY (`Article`) REFERENCES `product` (`Article`);

--
-- Ограничения внешнего ключа таблицы `movement_of_goods`
--
ALTER TABLE `movement_of_goods`
  ADD CONSTRAINT `Movement_of_goods_ibfk_1` FOREIGN KEY (`Product_article`) REFERENCES `product` (`Article`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `sale`
--
ALTER TABLE `sale`
  ADD CONSTRAINT `Sale_ibfk_1` FOREIGN KEY (`Product_article`) REFERENCES `product` (`Article`) ON DELETE CASCADE,
  ADD CONSTRAINT `Sale_ibfk_2` FOREIGN KEY (`Discount_Card_Number`) REFERENCES `discount_card` (`Discount_Card_Number`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
