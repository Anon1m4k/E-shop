-- phpMyAdmin SQL Dump
-- version 4.8.5
-- https://www.phpmyadmin.net/
--
-- Хост: localhost
-- Время создания: Ноя 27 2025 г., 14:36
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

--
-- Дамп данных таблицы `discount_card`
--

INSERT INTO `discount_card` (`Discount_Card_Number`, `Owner_FIO`, `Phone`, `Email`, `Discount_percentage`, `Active`) VALUES
(1001, 'Иванов Алексей Петрович', '+79161234567', 'ivanov@mail.ru', '0.05', 1),
(1002, 'Петрова Екатерина Сергеевна', '+79035556677', 'petrova@yandex.ru', '0.07', 1),
(1003, 'Сидоров Дмитрий Владимирович', '+79219876543', 'sidorov@gmail.com', '0.10', 1),
(1004, 'Козлова Анна Игоревна', '+79167778899', 'kozlova@mail.ru', '0.03', 1),
(1005, 'Николаев Максим Олегович', '+79038889900', 'nikolaev@yandex.ru', '0.08', 1);

-- --------------------------------------------------------

--
-- Структура таблицы `invoice`
--

CREATE TABLE `invoice` (
  `ID_Invoice` int(11) NOT NULL,
  `SerialNumber` varchar(50) NOT NULL,
  `Date` datetime NOT NULL,
  `CreatedAt` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `invoice`
--

INSERT INTO `invoice` (`ID_Invoice`, `SerialNumber`, `Date`, `CreatedAt`) VALUES
(1, 'TECH-INV-2024-001', '2024-11-21 10:00:00', '2025-11-27 14:19:30'),
(2, 'TECH-INV-2024-002', '2024-11-21 14:30:00', '2025-11-27 14:19:30'),
(3, 'TECH-INV-2024-003', '2024-11-22 09:15:00', '2025-11-27 14:19:30');

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
  `Quantity` int(50) NOT NULL,
  `Price` decimal(10,2) NOT NULL,
  `Unit` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Дамп данных таблицы `invoiceitems`
--

INSERT INTO `invoiceitems` (`ID_InvoiceItem`, `ID_Invoice`, `Article`, `Name`, `Category`, `Quantity`, `Price`, `Unit`) VALUES
(1, 1, 'TECH001', 'Смартфон Samsung Galaxy S23', 'Смартфоны', 10, '79990.00', 'шт'),
(2, 1, 'TECH002', 'Ноутбук ASUS VivoBook 15', 'Ноутбуки', 8, '54990.00', 'шт'),
(3, 1, 'TECH004', 'Наушники Sony WH-1000XM4', 'Аудиотехника', 15, '24990.00', 'шт'),
(4, 2, 'TECH005', 'Игровая консоль PlayStation 5', 'Игровые консоли', 5, '59990.00', 'шт'),
(5, 2, 'TECH010', 'Игровая мышь Razer DeathAdder', 'Компьютерные аксессуары', 20, '5990.00', 'шт'),
(6, 2, 'TECH011', 'Клавиатура Logitech MX Keys', 'Компьютерные аксессуары', 15, '8990.00', 'шт'),
(7, 3, 'TECH003', 'Телевизор LG 55\" 4K UHD', 'Телевизоры', 6, '69990.00', 'шт'),
(8, 3, 'TECH009', 'Монитор Dell 27\" IPS', 'Мониторы', 12, '28990.00', 'шт'),
(9, 3, 'TECH016', 'МФУ HP LaserJet Pro', 'Офисная техника', 7, '18990.00', 'шт');

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
('TECH001', 'Смартфон Samsung Galaxy S23', 'Смартфоны', '79990.00', 25, 'шт'),
('TECH002', 'Ноутбук ASUS VivoBook 15', 'Ноутбуки', '54990.00', 18, 'шт'),
('TECH003', 'Телевизор LG 55\" 4K UHD', 'Телевизоры', '69990.00', 12, 'шт'),
('TECH004', 'Наушники Sony WH-1000XM4', 'Аудиотехника', '24990.00', 30, 'шт'),
('TECH005', 'Игровая консоль PlayStation 5', 'Игровые консоли', '59990.00', 8, 'шт'),
('TECH006', 'Планшет Apple iPad Air', 'Планшеты', '65990.00', 15, 'шт'),
('TECH007', 'Умные часы Apple Watch Series 9', 'Умные часы', '32990.00', 20, 'шт'),
('TECH008', 'Фотоаппарат Canon EOS R50', 'Фототехника', '78990.00', 10, 'шт'),
('TECH009', 'Монитор Dell 27\" IPS', 'Мониторы', '28990.00', 22, 'шт'),
('TECH010', 'Игровая мышь Razer DeathAdder', 'Компьютерные аксессуары', '5990.00', 45, 'шт'),
('TECH011', 'Клавиатура Logitech MX Keys', 'Компьютерные аксессуары', '8990.00', 35, 'шт'),
('TECH012', 'Внешний жесткий диск Seagate 2TB', 'Накопители', '5990.00', 40, 'шт'),
('TECH013', 'Роутер TP-Link Archer AX55', 'Сетевое оборудование', '7990.00', 28, 'шт'),
('TECH014', 'Колонка JBL Flip 6', 'Аудиотехника', '8990.00', 32, 'шт'),
('TECH015', 'Электронная книга Amazon Kindle', 'Электронные книги', '12990.00', 25, 'шт'),
('TECH016', 'МФУ HP LaserJet Pro', 'Офисная техника', '18990.00', 14, 'шт'),
('TECH017', 'Веб-камера Logitech C920', 'Периферия', '6990.00', 38, 'шт'),
('TECH018', 'Микроволновая печь Samsung', 'Бытовая техника', '14990.00', 16, 'шт'),
('TECH019', 'Кофемашина DeLonghi', 'Бытовая техника', '29990.00', 9, 'шт'),
('TECH020', 'Дрон DJI Mini 3 Pro', 'Дроны и квадрокоптеры', '89990.00', 6, 'шт'),
('сс с', 'оулсолв', 'суоуот', '6.00', 0, 'шт'),
('сс сс', 'оулсолв', 'суоуот', '6.00', 6, 'шт');

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
  MODIFY `ID_Invoice` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT для таблицы `invoiceitems`
--
ALTER TABLE `invoiceitems`
  MODIFY `ID_InvoiceItem` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

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
