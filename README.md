# Rect orbit movement

Проект сделан в рамках технического задания.

Задача - сделать 2D прототип, где персонаж может перемещаться вокруг прямоугольной платформы, плавно огибая углы. Грань платформы всегда является "землей" для персонажа и он не может с нее упасть. Также персонаж должен уметь прыгать.

Демонстрация:

https://github.com/user-attachments/assets/3dd89e39-8eda-441f-9934-134368b1fe8c

## Реализация

Для реализации перемещения были выбраны Unity Splines. Замкнутый сплайн используется как путь, по которому перемещается персонаж.

Точки генерируются по часовой стрелке:

<img width="567" height="372" alt="points_clockwise" src="https://github.com/user-attachments/assets/82da58cc-eb3b-4299-b69c-7c1e5ecdb732" />

<details open>
    <summary>Реализация</summary>
    https://github.com/antonworkgit/rect-orbit-movement-demo/blob/fb29fd97d57eab1abf24743ed696618b65b9c3ad/Assets/Scripts/Utility/RoundedRectUtility.cs#L17-L79
</details>

Для удобства интерполяции в начало добавляется новая точка, которая будет центром пути (t=0)

<details open>
    <summary>Реализация</summary>
    https://github.com/antonworkgit/rect-orbit-movement-demo/blob/fb29fd97d57eab1abf24743ed696618b65b9c3ad/Assets/Scripts/Path/OrbPath.cs#L17-L41
</details>

В результате получается сплайн, выглядящий следующим образом:

<img width="438" height="286" alt="path" src="https://github.com/user-attachments/assets/6d530fdd-c202-4b57-b3fc-32c87a9099de" />

Использование Splines позволяет реализовывать дополнительные настройки, а также работать со встроенным редактором.

Для данной задачи это одно из множества подходящих решений и, наверное, одно из самых удобных в движке Unity.

Более оптимизированым решением будет использование простой геометрии:
1. Определить, к какому сегменту относится t или линейная позиция - к грани или дуге.
2. Рассчитать точку на грани или дуге соответственно.
