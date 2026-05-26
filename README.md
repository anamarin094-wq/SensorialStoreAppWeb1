<figure align="center">
<figcaption><b>Figura 1:</b>Visualización inicial de la app web Sensorial Store</figcaption>
<img width="1917" height="1016" alt="Captura de pantalla 2026-05-26 033756" src="https://github.com/user-attachments/assets/ed638cd4-c3f0-4a4f-b9b9-d016d64dbed7"/>
</figure>

<figure align="center">
<figcaption><b>Figura 2:</b>Formulario CRUD Productos</figcaption>
<img width="1919" height="1015" alt="Captura de pantalla 2026-05-26 034816" src="https://github.com/user-attachments/assets/4a9eec3e-9f21-45e2-bfce-1e2bee6b2ff2"/>
</figure>

<figure align="center">
<figcaption><b>Figura 3:</b>Visualización de registros de categorias</figcaption>
<img width="1919" height="1017" alt="Captura de pantalla 2026-05-26 035030" src="https://github.com/user-attachments/assets/2fd9716d-3b0c-4c0c-af94-65538a52fb1f"/>
</figure>

<figure align="center">
<figcaption><b>Figura 4:</b>Formulario CRUD Categorias</figcaption>
<img width="1919" height="1016" alt="Captura de pantalla 2026-05-26 035242" src="https://github.com/user-attachments/assets/eb584c10-b5b3-41a4-a7a6-6ba9ff9a3548"/>
</figure>

<figure align="center">
<figcaption><b>Figura 5:</b>Visualización de registros de marcas</figcaption>
<img width="1919" height="1015" alt="Captura de pantalla 2026-05-26 035332" src="https://github.com/user-attachments/assets/bc7565fa-400a-4d2a-87b3-744b496b7189"/>
</figure>

<figure align="center">
<figcaption><b>Figura 4:</b>Formulario CRUD Marcas</figcaption>
<img width="1919" height="1015" alt="Captura de pantalla 2026-05-26 035332" src="https://github.com/user-attachments/assets/3cc75eee-6edf-4919-aef9-4adf95fb47b5"/>
</figure>

## Base de Datos

CREATE DATABASE [SENSORIAL_STORE]
GO
USE [SENSORIAL_STORE]
GO

-- 1. CREACIÓN DE TABLAS
CREATE TABLE [dbo].[Categorias](
	[CategoriaId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NULL
)
GO

CREATE TABLE [dbo].[Marcas](
	[MarcaId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nombre] [varchar](50) NOT NULL,
	[PaisOrigen] [varchar](50) NULL
)
GO

CREATE TABLE [dbo].[Productos](
	[ProductoId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](max) NULL,
	[Precio] [decimal](18, 2) NOT NULL,
	[Stock] [int] NOT NULL DEFAULT ((0)),
	[CategoriaId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Categorias]([CategoriaId]),
	[MarcaId] [int] NOT NULL FOREIGN KEY REFERENCES [dbo].[Marcas]([MarcaId]),
	[ImagenUrl] [nvarchar](max) NULL
)
GO

-- 2. INSERCIÓN DE DATOS
SET IDENTITY_INSERT [dbo].[Categorias] ON 
INSERT [dbo].[Categorias] ([CategoriaId], [Nombre], [Descripcion]) VALUES 
(1, N'Labios', N'Productos para el cuidado y color de los labios'),
(2, N'Rostro', N'Bases, correctores, polvos y rubores'),
(3, N'Ojos', N'Sombras, pestañinas y delineadores'),
(4, N'Cejas', N'Betunes, lápices, geles fijadores y sombras para definir las cejas'),
(5, N'Preparación de Piel', N'Primers, aguas micelares, tónicos y fijadores de maquillaje')
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO

SET IDENTITY_INSERT [dbo].[Marcas] ON 
INSERT [dbo].[Marcas] ([MarcaId], [Nombre], [PaisOrigen]) VALUES 
(1, N'Maybelline', N'Estados Unidos'),
(2, N'Vogue', N'Colombia'),
(3, N'MAC Cosmetics', N'Canadá'),
(4, N'Ruby Rose', N'Brasil'),
(5, N'Trendy', N'Colombia'),
(6, N'Samy Cosmetics', N'Colombia'),
(7, N'Atenea', N'Colombia'),
(9, N'Ani-k', N'Colombia'),
(10, N'Prosa', N'México'),
(11, N'Purpure Makeup', N'Colombia')
SET IDENTITY_INSERT [dbo].[Marcas] OFF
GO

SET IDENTITY_INSERT [dbo].[Productos] ON 
INSERT [dbo].[Productos] ([ProductoId], [Nombre], [Descripcion], [Precio], [Stock], [CategoriaId], [MarcaId], [ImagenUrl]) VALUES 

(1, N'Labial SuperStay Matte Ink', N'Labial líquido de larga duración, acabado mate', 40000.00, 20, 1, 1, N'https://www.maybelline.co/-/media/project/loreal/brand-sites/mny/americas/latam/products/lip-makeup/lip-color/super-stay-matte-ink-liquid-lipstick/maybelline-lip-color-super-stay-matte-ink-dreamer-041554496901-o.jpg?rev=e1d042e1d5f641aabc5ce27b2883cc5e&cx=0.4&cy=0.61&cw=760&ch=1130&hash=97226466AFAAC1A958B78A266C0BEA25'),

(2, N'Base Fit Me Matte', N'Base de maquillaje de cobertura media con acabado natural', 38000.00, 15, 2, 1, N'https://palatsi.com.co/cdn/shop/files/238richtan_1_1024x.webp?v=1693081954'),

(3, N'Pestañina Efecto Total 6', N'Pestañina que alarga, da volumen y define', 18000.00, 30, 3, 2, N'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRz0o6agrHwiIHFffUHntlPNL_U3_BJv0Ua6Q&s'),

(4, N'Base Studio Fix Fluid SPF 15', N'Base de maquillaje de larga duración con control de grasa y acabado mate natural.', 185000.00, 7, 2, 3, N'https://blushbar.vtexassets.com/arquivos/ids/204355/773602642861_1.png?v=638497613967030000'),

(5, N'Gel Mascara Transparente', N'Gel transparente para peinar y fijar cejas y pestañas todo el día.', 14000.00, 40, 3, 10, N'https://cibermake.com/wp-content/uploads/2024/04/Pestanina_Prosa_Transparente.jpg'),

(6, N'Corrector Líquido Alta Cobertura Ani-k', N'Corrector ligero ideal para camuflar ojeras e imperfecciones sin acartonar la piel', 20000.00, 15, 2, 9, N'https://tutiendaprama.com/wp-content/uploads/2025/07/Diseno-sin-titulo-2-12.png'),

(7, N'Paleta de Sombras Meow Trendy', N'Paleta compacta con tonos ultra pigmentados, mates y satinados inspirados en gatitos.', 32000.00, 10, 3, 5, N'https://b2ctrendy.vtexassets.com/arquivos/ids/162729/STN-SMT1657-2.jpg?v=639032314261930000'),

(8, N'Fijador de Maquillaje Purpure Girl Boss', N'Bruma selladora de larga duración que unifica el maquillaje y da un acabado fresco.', 28500.00, 17, 2, 11, N'https://purpuremakeup.com/cdn/shop/files/Fijador_Purpure_Likeaboss_96912059-39bf-4c2c-9500-c25421efe8cb.webp?v=1743801020&width=720')

SET IDENTITY_INSERT [dbo].[Productos] OFF
GO


