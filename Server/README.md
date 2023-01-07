# Описание API

<style>
    th{
        background-color: rgba(1,1,1,.3)
    }
    td,th{
        border: 1px solid black;
    }
    .clr-cyan{
        background-color: rgba(1,200,200,.5)
    }
    a,a:visited{
        text-decoration: none;
        color: rgba(1,100,200,1);
    }
    a:hover{
        color: rgba(1,100,200,.7);
    }
    .req{
        font-weight: 600;
        color: rgba(1,100,150,1);
    }
</style>

## Склад

<table>
    <tr>
        <th>Метод</th>
        <th>Тип</th>
        <th>Описание</th>
        <th>Возвращаемое значение</th>
    </tr>
    <tr>
        <td>/store/get-list</td>
        <td><b>GET</b></td>
        <td>
            Возвращает информацию о складских остатках различных материалов</br>
            <b>Параметры</b>:</br>
            <u>name</u> - часть названия материала</br>
        </td>
        <td><a href="#storelist">StoreList</a></td>
    </tr>
    <tr>
        <td>/store/get</td>
        <td><b>GET</b></td>
        <td>
            Возвращает информацию о складских остатках определенного материала</br>
            <b>Параметры</b>:</br>
            <u>id</u> - идентификатор материала</br>
        </td>
        <td><a href="#store">Store</a></td>
    </tr>
</table>

<!--
| Метод      | Тип     | Описание | Возвращаемое значение |
| ---------- | ------- | -------- | --------------------- |
| /store/get | **GET** | Возвращает информацию о складских остатках<br/> ||
-->

## Производственный заказ

<table>
    <tr>
        <th>Метод</th>
        <th>Тип</th>
        <th>Описание</th>
        <th>Возвращаемое значение</th>
    </tr>
    <tr>
        <td>/order/get</td>
        <td><b>GET</b></td>
        <td>
            Возвращает информацию о конкретном ПЗ</br>
            <b>Параметры</b>:</br>
            <u>id</u> - идентификатор ПЗ</br>
        </td>
        <td><a href="#order">Order</a></td>
    </tr>
    <tr>
        <td>/order/get-list</td>
        <td><b>GET</b></td>
        <td>
            Возвращает информацию о различных ПЗ</br>
            <b>Параметры</b>:</br>
            <u>name</u> - часть названия ПЗ</br>
        </td>
        <td><a href="#orderlist">OrderList</a></td>
    </tr>
    <tr>
        <td>/order/create</td>
        <td><b>POST</b></td>
        <td>
            Создает и добавляет в БД новый ПЗ</br>
            <b>Запрос</b>: <a href="#createorder">CreateOrder</a>
        </td>
        <td><a href="#order">Order</a></td>
    </tr>
    <tr>
        <td>/order/modify</td>
        <td><b>POST</b></td>
        <td>
            Изменяет значения полей сущетсвующего ПЗ</br>
            <b>Запрос</b>: <a href="#modifyorders">ModifyOrders</a></br>
            Запрос будет менять поля у сущности с соответствующим идентификатором
        </td>
        <td><a href="#order">Order</a></td>
    </tr>
    <tr>
        <td>/order/approve</td>
        <td><b>POST</b></td>
        <td>
            Запрос на утверждение заказа</br>
            <b>Параметры</b>:</br>
            <u>id</u> - идентификатор ПЗ</br>
        </td>
        <td>-</td>
    </tr>
    <tr>
        <td>/order/complete</td>
        <td><b>POST</b></td>
        <td>
            Подтверждение выполнения части заказа</br>
            <b>Параметры</b>:</br>
            <u>id</u> - идентификатор части ПЗ</br>
        </td>
        <td>-</td>
    </tr>
    <tr>
        <td>/order/check</td>
        <td><b>POST</b></td>
        <td>
            Подтверждение проверки заказа</br>
            <b>Параметры</b>:</br>
            <u>id</u> - идентификатор части ПЗ</br>
        </td>
        <td>-</td>
    </tr>
    <tr>
        <td>/order/delete</td>
        <td><b>DELETE</b></td>
        <td>
            Удаляет ПЗ по указанному идентификатору</br>
            <b>Параметры</b>:</br>
            <u>id</u> - идентификатор заказа
        </td>
        <td>-</td>
    </tr>
</table>

<!--
| Метод         | Тип        | Описание | Возвращаемое значение |
| ------------- | ---------- | -------- | --------------------- |
| /order/get    | **GET**    |          |                       |
| /order/create | **POST**   |          |                       |
| /order/modify | **POST**   |          |                       |
| /order/delete | **DELETE** |          |                       |
-->

## Модели

<!--
Шаблон
<table>
    <tr>
        <th>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td></td>
    </tr>
</table>
-->

### StoreList

<table>
    <tr>
        <th colspan=2>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td colspan=2>list</td>
        <td>store[]</td>
        <td>Список материалов</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>id</td>
        <td>guid</td>
        <td>Идентификатор материала</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>name</td>
        <td>string</td>
        <td>Название материала</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>count_available</td>
        <td>int</td>
        <td>Доступное кол-во материала</td>
    </tr>
</table>

### Store

<table>
    <tr>
        <th>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td>id</td>
        <td>guid</td>
        <td>Идентификатор материала</td>
    </tr>
    <tr>
        <td>name</td>
        <td>string</td>
        <td>Название материала</td>
    </tr>
    <tr>
        <td>count_stored</td>
        <td>int</td>
        <td>Кол-во материала, хранящееся на складе</td>
    </tr>
    <tr>
        <td>count_reserved</td>
        <td>int</td>
        <td>Зарезервированное кол-во материала</td>
    </tr>
    <tr>
        <td>count_available</td>
        <td>int</td>
        <td>
            Доступное кол-во материала:</br>
            <i>count_stored</i> - <i>count_reserved</i>
        </td>
    </tr>
</table>

### Order

<table>
    <tr>
        <th colspan=3>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td colspan=3>id</td>
        <td>guid</td>
        <td>Идентификатор заказа</td>
    </tr>
    <tr>
        <td colspan=3>name</td>
        <td>string</td>
        <td>Название заказа</td>
    </tr>
    <tr>
        <td colspan=3>date_reg</td>
        <td>date</td>
        <td>Дата регистрации заказа</td>
    </tr>    
    <tr>
        <td colspan=3>parts</td>
        <td>part[]</td>
        <td>Части заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>id</td>
        <td>guid</td>
        <td>Идентификатор части заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>order_num</td>
        <td>int</td>
        <td>Порядковый номер</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>is_completed</td>
        <td>bool</td>
        <td>Часть выполнена</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>date_end</td>
        <td>date</td>
        <td>Дата окончания выполнения</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>storelist</td>
        <td>store[]</td>
        <td>Список используемых материалов</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>id</td>
        <td>guid</td>
        <td>Идентификатор материала</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>name</td>
        <td>string</td>
        <td>Название материала</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>count</td>
        <td>int</td>
        <td>Используемое кол-во материала</td>
    </tr>
    <tr>
        <td colspan=3>is_approved</td>
        <td>bool</td>
        <td>Заказ утвержден</td>
    </tr>
    <tr>
        <td colspan=3>is_completed</td>
        <td>bool</td>
        <td>Заказ выполнен</td>
    </tr>
    <tr>
        <td colspan=3>is_checked</td>
        <td>bool</td>
        <td>Заказ проверен</td>
    </tr>
</table>

### OrderList

<table>
    <tr>
        <th colspan=2>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td colspan=2>list</td>
        <td>order[]</td>
        <td></td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>id</td>
        <td>guid</td>
        <td>Идентификатор заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>name</td>
        <td>string</td>
        <td>Название заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>date_reg</td>
        <td>date</td>
        <td>Дата регистрации заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td>is_completed</td>
        <td>bool</td>
        <td>Заказ завершен</td>
    </tr>
</table>

### CreateOrder

<table>
    <tr>
        <th colspan=3>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td colspan=3>name</td>
        <td>string</td>
        <td>Название заказа</td>
    </tr>
    <tr>
        <td colspan=3>parts</td>
        <td>part[]</td>
        <td>Части заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>order_num</td>
        <td>int</td>
        <td>Порядковый номер</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>date_end</td>
        <td>date</td>
        <td>Дата окончания выполнения</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>storelist</td>
        <td>store[]</td>
        <td>Список используемых материалов</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>id</td>
        <td>guid</td>
        <td>Идентификатор материала</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>count</td>
        <td>int</td>
        <td>Используемое кол-во материала</td>
    </tr>
</table>

### ModifyOrders

<table>
    <tr>
        <th colspan=3>Название</th>
        <th>Тип данных</th>
        <th>Описание</th>
    </tr>
    <tr>
        <td colspan=3>orders</td>
        <td>order[]</td>
        <td>Заказы</td>
    </tr>
    <tr class="req">
        <td class="clr-cyan"></td>
        <td colspan=2>id</td>
        <td>guid</td>
        <td>Идентификатор заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>name</td>
        <td>string</td>
        <td>Название заказа</td>
    </tr>
    <tr>
        <td colspan=3>parts</td>
        <td>part[]</td>
        <td>Части заказа</td>
    </tr>
    <tr class="req">
        <td class="clr-cyan"></td>
        <td colspan=2>id</td>
        <td>guid</td>
        <td>Идентификатор части заказа</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>order_num</td>
        <td>int</td>
        <td>Порядковый номер</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>date_end</td>
        <td>date</td>
        <td>Дата окончания выполнения</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td colspan=2>storelist</td>
        <td>store[]</td>
        <td>Используемые материалы</td>
    </tr>
    <tr class="req">
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>id</td>
        <td>guid</td>
        <td>Идентификатор материала</td>
    </tr>
    <tr>
        <td class="clr-cyan"></td>
        <td class="clr-cyan"></td>
        <td>count</td>
        <td>int</td>
        <td>Используемое кол-во материала</td>
    </tr>
</table>
