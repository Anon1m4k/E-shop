-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Окт 12 2025 г., 10:45
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
-- Структура таблицы `Discount_card`
--

CREATE TABLE `Discount_card` (
  `Discount_Card_Number` int(20) NOT NULL,
  `Owner_FIO` varchar(100) DEFAULT NULL,
  `Phone` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Discount_percentage` decimal(3,2) DEFAULT NULL,
  `Active` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `Movement_of_goods`
--

CREATE TABLE `Movement_of_goods` (
  `ID_Movement` int(11) NOT NULL,
  `Product_article` varchar(255) DEFAULT NULL,
  `Operation_type` enum('Приход','Расход') DEFAULT NULL,
  `Quantity` int(50) DEFAULT NULL,
  `Unit` varchar(10) DEFAULT NULL,
  `Data` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `Sale`
--

CREATE TABLE `Sale` (
  `ID_Sales` int(11) NOT NULL,
  `Date_Sales` datetime DEFAULT NULL,
  `Product_article` varchar(255) DEFAULT NULL,
  `Quantity` int(50) DEFAULT NULL,
  `Price_per_Unit` decimal(10,2) DEFAULT NULL,
  `Discount_Card_Number` int(20) DEFAULT NULL,
  `Discount_amount` decimal(10,2) DEFAULT NULL,
  `Total_sum` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `Product`
--

CREATE TABLE `Product` (
  `Article` varchar(255) NOT NULL,
  `Name` varchar(500) DEFAULT NULL,
  `Category` varchar(50) DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  `Stock` int(50) DEFAULT NULL,
  `Unit` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `Product`
--

INSERT INTO `Product` (`Article`, `Name`, `Category`, `Price`, `Stock`, `Unit`) VALUES
('1', 'Клавиатура', 'Техника', '1500.00', 5, 'шт'),
('2', 'Компьютер', 'Техника', '15000.00', 4, 'шт'),
('3', 'Мышь', 'Техника', '500.00', 10, 'шт');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `Discount_card`
--
ALTER TABLE `Discount_card`
  ADD PRIMARY KEY (`Discount_Card_Number`);

--
-- Индексы таблицы `Movement_of_goods`
--
ALTER TABLE `Movement_of_goods`
  ADD PRIMARY KEY (`ID_Movement`),
  ADD KEY `Product_article` (`Product_article`);

--
-- Индексы таблицы `Sale`
--
ALTER TABLE `Sale`
  ADD PRIMARY KEY (`ID_Sales`),
  ADD KEY `Product_article` (`Product_article`),
  ADD KEY `Discount_Card_Number` (`Discount_Card_Number`);

--
-- Индексы таблицы `Product`
--
ALTER TABLE `Product`
  ADD PRIMARY KEY (`Article`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `Movement_of_goods`
--
ALTER TABLE `Movement_of_goods`
  MODIFY `ID_Movement` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `Sale`
--
ALTER TABLE `Sale`
  MODIFY `ID_Sales` int(11) NOT NULL AUTO_INCREMENT;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `Movement_of_goods`
--
ALTER TABLE `Movement_of_goods`
  ADD CONSTRAINT `Movement_of_goods_ibfk_1` FOREIGN KEY (`Product_article`) REFERENCES `Product` (`Article`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `Sale`
--
ALTER TABLE `Sale`
  ADD CONSTRAINT `Sale_ibfk_1` FOREIGN KEY (`Product_article`) REFERENCES `Product` (`Article`) ON DELETE CASCADE,
  ADD CONSTRAINT `Sale_ibfk_2` FOREIGN KEY (`Discount_Card_Number`) REFERENCES `Discount_card` (`Discount_Card_Number`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;