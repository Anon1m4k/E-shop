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
-- База данных: `интернет_магазин_`
--

-- --------------------------------------------------------

--
-- Структура таблицы `дисконтнаякарта`
--

CREATE TABLE `дисконтнаякарта` (
  `Номер_Дисконтной_Карты` int(20) NOT NULL,
  `ФИО_Владельца` varchar(100) DEFAULT NULL,
  `Телефон` varchar(15) DEFAULT NULL,
  `Email` varchar(100) DEFAULT NULL,
  `Процент_Скидки` decimal(3,2) DEFAULT NULL,
  `Активна` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `движениетоваров`
--

CREATE TABLE `движениетоваров` (
  `ID_Движения` int(11) NOT NULL,
  `Артикул_Товара` varchar(255) DEFAULT NULL,
  `Тип_Операции` enum('Приход','Расход') DEFAULT NULL,
  `Количество` int(50) DEFAULT NULL,
  `Ед_измерения` varchar(10) DEFAULT NULL,
  `Дата_Время` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `продажа`
--

CREATE TABLE `продажа` (
  `ID_Продажи` int(11) NOT NULL,
  `Дата_Продажи` datetime DEFAULT NULL,
  `Артикул_Товара` varchar(255) DEFAULT NULL,
  `Количество` int(50) DEFAULT NULL,
  `Цена_За_Единицу` decimal(10,2) DEFAULT NULL,
  `Номер_Дисконтной_Карты` int(20) DEFAULT NULL,
  `Сумма_Скидки` decimal(10,2) DEFAULT NULL,
  `Итоговая_Сумма` decimal(10,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

-- --------------------------------------------------------

--
-- Структура таблицы `товар`
--

CREATE TABLE `товар` (
  `Артикул_Товара` varchar(255) NOT NULL,
  `Наименование` varchar(500) DEFAULT NULL,
  `Категория` varchar(50) DEFAULT NULL,
  `Цена` decimal(10,2) DEFAULT NULL,
  `Остаток` int(50) DEFAULT NULL,
  `Ед_измерения` varchar(10) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `товар`
--

INSERT INTO `товар` (`Артикул_Товара`, `Наименование`, `Категория`, `Цена`, `Остаток`, `Ед_измерения`) VALUES
('1', 'Клавиатура', 'Техника', '1500.00', 5, 'шт'),
('2', 'Компьютер', 'Техника', '15000.00', 4, 'шт'),
('3', 'Мышь', 'Техника', '500.00', 10, 'шт');

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `дисконтнаякарта`
--
ALTER TABLE `дисконтнаякарта`
  ADD PRIMARY KEY (`Номер_Дисконтной_Карты`);

--
-- Индексы таблицы `движениетоваров`
--
ALTER TABLE `движениетоваров`
  ADD PRIMARY KEY (`ID_Движения`),
  ADD KEY `Артикул_Товара` (`Артикул_Товара`);

--
-- Индексы таблицы `продажа`
--
ALTER TABLE `продажа`
  ADD PRIMARY KEY (`ID_Продажи`),
  ADD KEY `Артикул_Товара` (`Артикул_Товара`),
  ADD KEY `Номер_Дисконтной_Карты` (`Номер_Дисконтной_Карты`);

--
-- Индексы таблицы `товар`
--
ALTER TABLE `товар`
  ADD PRIMARY KEY (`Артикул_Товара`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `движениетоваров`
--
ALTER TABLE `движениетоваров`
  MODIFY `ID_Движения` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT для таблицы `продажа`
--
ALTER TABLE `продажа`
  MODIFY `ID_Продажи` int(11) NOT NULL AUTO_INCREMENT;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `движениетоваров`
--
ALTER TABLE `движениетоваров`
  ADD CONSTRAINT `движениетоваров_ibfk_1` FOREIGN KEY (`Артикул_Товара`) REFERENCES `товар` (`Артикул_Товара`) ON DELETE CASCADE;

--
-- Ограничения внешнего ключа таблицы `продажа`
--
ALTER TABLE `продажа`
  ADD CONSTRAINT `продажа_ibfk_1` FOREIGN KEY (`Артикул_Товара`) REFERENCES `товар` (`Артикул_Товара`) ON DELETE CASCADE,
  ADD CONSTRAINT `продажа_ibfk_2` FOREIGN KEY (`Номер_Дисконтной_Карты`) REFERENCES `дисконтнаякарта` (`Номер_Дисконтной_Карты`) ON DELETE SET NULL;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
